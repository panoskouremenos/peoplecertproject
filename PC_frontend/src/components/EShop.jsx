import React, { useState } from "react";
import { Form } from 'react-bootstrap';
import DatePicker from 'react-datepicker'; 
import "../Eshop.css";

const EShop = ({ eshop, onadd, onremove }) => {
    const [examDate, setExamDate] = useState('');
    const maxDate = new Date();
    maxDate.setMonth(maxDate.getMonth() + 1);
    const maxDateISOString = maxDate.toISOString().slice(0, 16);

    const handleDateChange = (e) => {
        const selectedDate = new Date(e.target.value + ":00"); 
        const greekDate = new Date(selectedDate.toLocaleString('en-US', { timeZone: 'Europe/Athens' }));
        setExamDate(greekDate.toISOString().slice(0, 16));
    };

    return (
        <>
            {eshop.isAlreadyBought === false ? (
                <div className={`${eshop.inBasket === false ? "card m-1" : "card m-1 inBasket"}`}>
                    <div className="card-body">
                        <h5 className="card-title">{eshop.productName}</h5>
                        <p className="card-text">{eshop.description}</p>
                        <DatePicker
                            selected={examDate}
                            onChange={(date) => {setExamDate(date)}}
                            showTimeSelect
                            dateFormat="Pp"
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
