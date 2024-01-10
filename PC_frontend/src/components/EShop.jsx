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
                <h5 className="card-title">{eshop.ProductName}</h5>
                <p className="card-text">{eshop.Description}</p>
                <p className="card-price"><strong>${eshop.Price}</strong></p>
                <p className="card-availability">Available Stock: {eshop.AvailableStock}</p>
                <Form.Group className="mb-3">
                <Form.Label>Exam Date:</Form.Label>
                <Form.Control
                    type="datetime-local"
                    name="examDate"
                    min={new Date().toISOString().split('.')[0]}
                    max={maxDateISOString}
                    value={examDate}
                    onChange={(e) => setExamDate(formatDatetime(e.target.value))}
                    step="60"
                />
                </Form.Group>
                {eshop.inBasket === false ?
                <button className="btn btn-add" onClick={(e) => onadd(eshop.ProductID , examDate)}>Add to Cart</button>
                :
                <button className="btn btn-danger" onClick={(e) => onremove(eshop.ProductID)}>Remove from Cart</button>
                }
            </div>
        </div>
    );
}

export default EShop;
