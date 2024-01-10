import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faShoppingCart } from "@fortawesome/free-solid-svg-icons";
import Eshop from "./EShop";

const EShopList = ({ eshops, boughtProducts , onadd, onremove , onbuy }) => {
  const basketCount = eshops.filter((cert) => cert.inBasket === true).length;

  return (
    <div className="container mt-4">
      <div className="row">
        <div className="col-md-10 text-center">
          <h1>Certification Voucher Shop</h1>
        </div>
        <div className="shopbasket_container col d-flex align-items-center">
          <button className="shopBasketCount" title="Finish Purchase" onClick={onbuy}>
            <FontAwesomeIcon icon={faShoppingCart} className="mr-2" />
            <span className="font-weight-bold">{basketCount}</span>
          </button>
        </div>
      </div>
      <div className="shoplist row mt-3">
        {eshops.map((eshop) => (
          <Eshop
            key={eshop.productId}
            onremove={onremove}
            onadd={onadd}
            eshop={eshop}
          />
        ))}
        {boughtProducts.map((eshop) => {
          return(
          <div className="card bought m-1" key={eshop.productId}>
          <div className="card-body">
              <h5 className="card-title">{eshop.productName}</h5>
              <p className="card-text">{eshop.description}</p>
          </div>
      </div>)
        })}
      </div>
    </div>
  );
};

export default EShopList;
