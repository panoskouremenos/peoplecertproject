import React, { useContext, useState, useEffect } from "react";
import AuthContext from "../AuthContext";
import EShopList from "./EShopList";
import CloseButton from "./CloseButton";

const EShopPage = () => {
  const [productsToBuy, setProductsToBuy] = useState([]);
  const [productsBought, setProductsBought] = useState([]);
  const [purchaseBox, setPurchaseBox] = useState(false);
  const { user, token } = useContext(AuthContext);

  const fetchProducts = async () => {
    try {
      const response = await fetch("http://localhost:5888/api/EshopProducts", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      });

      if (response.ok) {
        const products = await response.json();
        setProductsToBuy(
          products.map((product) => ({ ...product, inBasket: false }))
        );

        // Fetch user's products only if there's an authenticated user
        if (token) {
          fetchUserProducts(token);
        }
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
    }
  };

  const fetchUserProducts = async (token) => {
    try {
      const response = await fetch("http://localhost:5888/api/user/certificates", {
        method: "GET",
        headers: {
          "Authorization": token,
          "Content-Type": "application/json",
        },
      });

      if (response.ok) {
        const products = await response.json();

        setProductsToBuy((prevProducts) => {
          const updatedProducts = prevProducts.filter((prod) => !products.some((p) => p.ProductID === prod.ProductID));
          return updatedProducts.map((product) => ({ ...product, inBasket: false }));
        });

        setProductsBought(products.map((product) => ({ ...product })));
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, [token]);
  

  const handleBuyCertificate = (id) => {
    const productIndex = productsToBuy.findIndex(
      (product) => product.ProductID === id
    );

    if (productIndex !== -1) {
      if (!productsToBuy[productIndex].inBasket) {
        setProductsToBuy((prevProducts) => {
          const updatedProducts = [...prevProducts];
          updatedProducts[productIndex].inBasket = true;
          return updatedProducts;
        });
      }
    } else {
      alert("Product not found.");
    }
  };

  const handleRemoveCertificate = (id) => {
    const productIndex = productsToBuy.findIndex(
      (product) => product.ProductID === id
    );
    if (productIndex !== -1) {
      setProductsToBuy((prevProducts) => {
        const updatedProducts = [...prevProducts];
        updatedProducts[productIndex].inBasket = false;
        return updatedProducts;
      });
    }
  };

  const handleBoughtProducts = () => {
    let counted = 0;
    productsToBuy.filter((prod) => {
      prod.inBasket === true ? counted++ : "";
    });
    if (counted > 0) {
      setPurchaseBox(true);
    }else{
      setPurchaseBox(false);
    }
  };

  const buyProducts = async (token) => {
    const productsToPurchase = productsToBuy
      .filter((prod) => prod.inBasket === true)
      .map((prod) => prod.ProductID);
  
    if (productsToPurchase.length > 0) {
      try {
        const response = await fetch("http://localhost:3001/api/certificates/buy", {
          method: "POST",
          headers: {
            Authorization: token,
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            products: productsToPurchase,
          }),
        });
  
        if (response.status === 200) {
          const result = await response.json();
  
          setProductsToBuy((prevProductsToBuy) => {
            const updatedProducts = prevProductsToBuy.map((prod) => {
              if (prod.inBasket) {
                return { ...prod, inBasket: false };
              }
              return prod;
            });
  
            return updatedProducts.filter((prod) => !productsToPurchase.includes(prod.ProductID));
          });
  
          setProductsBought((prevBoughtProducts) => {
            const productsToAdd = productsToBuy
              .filter((prod) => prod.inBasket === true)
              .map((prod) => ({ ...prod }));
  
            return [...prevBoughtProducts, ...productsToAdd];
          });
  
          setPurchaseBox(false);
          alert(result.success || 'Purchase was successful!');
        } else {
          alert('Problem..');
        }
      } catch (error) {
        console.error("Error fetching user data:", error);
        alert("An error occurred during the purchase.");
      }
    }
  };
  
  
  
  

  const handlePurchaseProducts = () => {
    buyProducts(token);
  };

  return (
    <>
      <div id="eshop">
        <EShopList
          onadd={handleBuyCertificate}
          onremove={handleRemoveCertificate}
          onbuy={handleBoughtProducts}
          boughtProducts={productsBought}
          eshops={productsToBuy}
        />
      </div>
      {purchaseBox === true ? (
        <div id="purchaseContainer">
          <CloseButton
            onClick={() => {
              setPurchaseBox(false);
            }}
          />
          <div className="container">
            <table className="table">
              <thead>
                <tr>
                  <th scope="col">Name</th>
                  <th scope="col">Price</th>
                </tr>
              </thead>
              <tbody>
                {productsToBuy.map((product) => {
                  if (product.inBasket === true) {
                    return (
                      <tr key={product.ProductID}>
                        <td>{product.ProductName}</td>
                        <td className="text-danger">${product.Price}</td>
                      </tr>
                    );
                  }
                })}

                <tr>
                  <td>Total Price : </td>
                  <td className="fw-bold text-danger">
                    $
                    {productsToBuy
                      .filter((prod) => prod.inBasket === true)
                      .map((prod) => prod.Price)
                      .reduce((acc, price) => acc + price, 0)}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <button className="btn btn-add" onClick={handlePurchaseProducts}>
            Purchase?
          </button>
        </div>
      ) : (
        ""
      )}
    </>
  );
};

export default EShopPage;
