import React, { useContext, useState, useEffect } from "react";
import AuthContext from "../AuthContext";
import CandidateContext from "../CandidateContext";
import AlertContext from "../AlertContext";
import EShopList from "./EShopList";
import CloseButton from "./CloseButton";

const EShopPage = () => {
  const [productsToBuy, setProductsToBuy] = useState([]);
  const [productsBought, setProductsBought] = useState([]);
  const [purchaseBox, setPurchaseBox] = useState(false);
  const { token } = useContext(AuthContext);
  const { alerts , setAlerts } = useContext(AlertContext);
  const { isCandidate , setIsCandidate } = useContext(CandidateContext);

  const fetchProducts = async () => {
    try {
      const response = await fetch("https://localhost:5888/api/EshopProducts", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`,
        },
      });
  
      if (response.ok) {
        const products = await response.json();
        const initialProductsToBuy = products.map((product) => ({ ...product, inBasket: false, isAlreadyBought: false }));
        setProductsToBuy(initialProductsToBuy);
  
        if (token) {
          fetchUsersProducts(token, initialProductsToBuy);
        }
      }
    } catch (error) {
      console.error("Error fetching user data:", error);
    }
  };
  
  const fetchUsersProducts = async (token, initialProductsToBuy) => {
    try {
      const response = await fetch("https://localhost:5888/api/EshopProducts/MyPurchases", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`
        }
      });
  
      if (response.ok) {
        const result = await response.json();
        console.log(result);
  
        const updatedProducts = initialProductsToBuy.map(product => (
          result.includes(product.productId) ? { ...product, isAlreadyBought: true } : product
        ));
        console.log(updatedProducts);
        setProductsToBuy(updatedProducts);
      } else {
        console.log(response.status);
        console.log('Response not okay');
      }
    } catch (error) {
      console.log(error);
    } finally {
    }
  };
  
  useEffect(() => {
    fetchProducts();
  }, [token]);
  

  const handleBuyCertificate = (id, examDate) => {
    const newAlerts = [];
    
    const productIndex = productsToBuy.findIndex(
      (product) => product.productId === id
    );
  
    if (productIndex !== -1) {
          setProductsToBuy((prevProducts) => {
            const updatedProducts = [...prevProducts];
            updatedProducts[productIndex].examDate = examDate;
            updatedProducts[productIndex].inBasket = true;
            return updatedProducts;
          });
          newAlerts.push({
            variant: "success",
            message: "Product added to the basket.",
          });
        } else {
          newAlerts.push({
            variant: "danger",
            message:
              "The examination date must be between tomorrow and at most 1 month after.",
          });
        }
    setAlerts(newAlerts);
  };
  

  const handleRemoveCertificate = (id) => {
    const newAlerts = [];
    const productIndex = productsToBuy.findIndex(
      (product) => product.productId === id
    );
    if (productIndex !== -1) {
      setProductsToBuy((prevProducts) => {
        const updatedProducts = [...prevProducts];
        delete updatedProducts[productIndex].examDate;
        updatedProducts[productIndex].inBasket = false;
        return updatedProducts;
      });
      newAlerts.push({
        variant: "success",
        message: "Product removed from the basket.",
      });
    }
    setAlerts(newAlerts);
  };

  const handleBoughtProducts = () => {
    let counted = productsToBuy.filter((prod) => prod.inBasket === true).length;
    setPurchaseBox(counted > 0);
  };

  const buyProducts = async (token) => {
    const newAlerts = [];
    const productsToPurchase = productsToBuy
      .filter((prod) => prod.inBasket === true)
      .map(({ productId, examDate }) => ({ productId: productId, purchaseDate : examDate }));

    if (productsToPurchase.length > 0) {
      try {
        console.log(JSON.stringify([...productsToPurchase]));
        const response = await fetch("https://localhost:5888/api/EshopProducts/PurchaseAndExam", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            'Authorization': `Bearer ${token}`,
          },
          body: JSON.stringify([...productsToPurchase]),
        });

        if (response.status == 200) {
          setProductsToBuy((prevProductsToBuy) => {
            const updatedProducts = prevProductsToBuy.map((prod) => {
              if (prod.inBasket) {
                return { ...prod, inBasket: false };
              }
              return prod;
            });
  
            return updatedProducts.filter((prod) => !productsToPurchase.find(p => p.productId === prod.productId));
          });
  
          setProductsBought((prevBoughtProducts) => {
            const productsToAdd = productsToBuy
              .filter((prod) => prod.inBasket === true)
              .map((prod) => ({ ...prod }));
  
            return [...prevBoughtProducts, ...productsToAdd];
          });
  
          setPurchaseBox(false);
          newAlerts.push({
            variant: "success",
            message: "Purchase was successful!",
          });
        } else {
          newAlerts.push({
            variant: "danger",
            message: "You need to be a candidate to buy!",
          });
        }
        setAlerts(newAlerts);
      } catch (error) {
        console.error("Error fetching user data:", error);
      }
    }
  };

  const handlePurchaseProducts = () => {
    if(token){
      buyProducts(token);
    }else{
      setAlerts([{
        variant: "danger",
        message: "You need to first login in order to purchase.",
      }])
    }
  };

  return (
    <>
      <div id="eshop">
        {isCandidate !== null ? "" : (
          <>
          <h5 className="text-center text-danger">You need to be a candidate to shop,&nbsp;
          <a href="http://localhost:5173/user/cp">become one</a>
          </h5>
          </>
        )}
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
                  <th scope="col">Exam Date</th>
                  <th scope="col">Price</th>
                </tr>
              </thead>
              <tbody>
                {productsToBuy.map((product) => {
                  if (product.inBasket === true) {
                    return (
                      <tr key={product.productId}>
                        <td>{product.productName}</td>
                        <td>{new Date(product.examDate).toLocaleDateString('el-GR', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                      })}</td>
                        <td className="text-danger">${product.price}</td>
                      </tr>
                    );
                  }
                })}

                <tr>
                  <td colspan="2">Total Price : </td>
                  <td className="fw-bold text-danger">
                    $
                    {productsToBuy
                      .filter((prod) => prod.inBasket === true)
                      .map((prod) => prod.price)
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
