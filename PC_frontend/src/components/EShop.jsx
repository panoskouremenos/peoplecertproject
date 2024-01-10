import React, { useState } from "react";
import { Form } from 'react-bootstrap';
import "../Eshop.css";

const EShop = ({ eshop , onadd , onremove }) => {
    const [examDate, setExamDate] = useState('');
    const formatDatetime = (date) => {
        const formattedDate = new Date(date);
        formattedDate.setSeconds(0);
        return formattedDate.toISOString().slice(0, 16); 
    };
    const maxDate = new Date();
    maxDate.setMonth(maxDate.getMonth() + 1);
    const maxDateISOString = maxDate.toISOString().slice(0, 16);

    return (
        <div className={eshop.inBasket === false ? `card m-1` : `card m-1 inBasket`}>
            <div className="card-body">
                <h5 className="card-title">{eshop.productName}</h5>
                <p className="card-text">{eshop.description}</p>
                <p className="card-price"><strong>${eshop.price}</strong></p>
                <p className="card-availability">Available Stock: {eshop.availableStock}</p>
                {eshop.inBasket === false ?
                <button className="btn btn-add" onClick={(e) => onadd(eshop.productId)}>Add to Cart</button>
                :
                <button className="btn btn-danger" onClick={(e) => onremove(eshop.productId)}>Remove from Cart</button>
                }
            </div>
        </div>
    );
}

export default EShop;
