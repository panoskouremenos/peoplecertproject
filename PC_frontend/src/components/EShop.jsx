import React, { useState } from "react";
import { Form } from 'react-bootstrap';
import "../Eshop.css";

const EShop = ({ eshop, onadd, onremove }) => {
    const [examDate, setExamDate] = useState('');
    const maxDate = new Date();
    maxDate.setMonth(maxDate.getMonth() + 1);
    const maxDateISOString = maxDate.toISOString().slice(0, 16);

    const handleDateChange = (e) => {
        setExamDate(e.target.value);
    };

    return (
        <>
            {eshop.isAlreadyBought === false ? (
                <div className={`${eshop.inBasket === false ? "card m-1" : "card m-1 inBasket"}`}>
                    <div className="card-body">
                        <h5 className="card-title">{eshop.productName}</h5>
                        <p className="card-text">{eshop.description}</p>
                        <Form.Control
                            type="datetime-local"
                            value={examDate}
                            onChange={handleDateChange}
                            min={new Date().toISOString().slice(0, 16)} // To disable past dates
                            max={maxDateISOString}
                        />
                        <p className="card-price"><strong>${eshop.price}</strong></p>
                        <p className="card-availability">Available Stock: {eshop.availableStock}</p>
                        {eshop.inBasket === false ?
                            <button className="btn btn-add" onClick={(e) => onadd(eshop.productId, examDate)}>Add to Cart</button>
                            :
                            <button className="btn btn-danger" onClick={(e) => onremove(eshop.productId)}>Remove from Cart</button>
                        }
                    </div>
                </div>
            ) : (
                <div className="card bought m-1" key={eshop.productId}>
                    <div className="card-body">
                        <h5 className="card-title">{eshop.productName}</h5>
                        <p className="card-text">{eshop.description}</p>
                    </div>
                </div>
            )}
        </>
    );
}

export default EShop;
