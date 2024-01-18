import React, { useState, useContext } from 'react';
import DatePicker from 'react-datepicker'; 
import 'react-datepicker/dist/react-datepicker.css'; 
import AuthContext from '../../AuthContext';
import AlertContext from '../../AlertContext';
import { format } from 'date-fns';

const RedeemVoucher = () => {
    const [voucherCode, setVoucherCode] = useState('');
    const [examDate, setExamDate] = useState(new Date());
    const { token } = useContext(AuthContext);
    const { Alerts , setAlerts } = useContext(AlertContext);

    const handleRedeem = async () => {
        const newAlerts = [];
        try {
            const formattedDate = format(examDate, 'yyyyMMddHHmm');
            const response = await fetch(`https://localhost:5888/api/ExamVouchers/RedeemVoucher/${voucherCode}`, {
                method: 'PUT',
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify({ dateAssigned: formattedDate })
            });

            if (!response.ok) {
                const errorText = await response.text();
                newAlerts.push({ variant : "danger" , message : `Response Status: ${response.status}, Response Text: ${errorText}`});
                throw new Error('Network response was not ok');
            }
            newAlerts.push({ variant : "success" , message : 'Voucher redeemed successfully!'});
            setVoucherCode('');
            setExamDate(new Date());
        } catch (error) {
            newAlerts.push({ variant : "danger" , message : `Error redeeming voucher: ${error}`});
        }finally{
            setAlerts(newAlerts);
        }
    };
    
    

    return (
        <div className="container">
            <h2>Redeem Voucher</h2>
            <div>
                <input
                    type="text"
                    className="form-control mb-1"
                    placeholder="Enter Voucher Code"
                    value={voucherCode}
                    onChange={(e) => setVoucherCode(e.target.value)}
                />
            </div>
            <div>
                <DatePicker
                    selected={examDate}
                    onChange={(date) => {setExamDate(date)}}
                    showTimeSelect
                    className="mb-1"
                    dateFormat="Pp"
                />
            </div>
            <div>
                <button onClick={handleRedeem} className="btn btn-primary">
                    Redeem
                </button>
            </div>
        </div>
    );
};

export default RedeemVoucher;
