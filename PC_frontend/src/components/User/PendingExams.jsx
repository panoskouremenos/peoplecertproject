import React, { useState, useContext } from 'react';
import DatePicker from 'react-datepicker'; // make sure to install react-datepicker
import 'react-datepicker/dist/react-datepicker.css'; // Default styling
import AuthContext from '../../AuthContext'; // Adjust path as needed
import { format } from 'date-fns';

const RedeemVoucher = () => {
    const [voucherCode, setVoucherCode] = useState('');
    const [examDate, setExamDate] = useState(new Date());
    const { token } = useContext(AuthContext);

    const handleRedeem = async () => {
        try {
            const formattedDate = format(examDate, 'yyyy-MM-ddTHH:mm:ss'); // Adjust the date format
    
            console.log("Formatted Date:", formattedDate);
            console.log("Voucher Code:", voucherCode);
    
            const response = await fetch(`https://localhost:5888/api/ExamVouchers/RedeemVoucher/${voucherCode}`, {
                method: 'PUT',
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify({ redeemVoucherDto: { dateAssigned: formattedDate } }) // Adjusted structure
            });
    
            if (!response.ok) {
                const errorText = await response.text();
                console.error('Response Status:', response.status, 'Response Text:', errorText);
                throw new Error('Network response was not ok');
            }
    
            alert('Voucher redeemed successfully!');
        } catch (error) {
            console.error('Error redeeming voucher:', error);
            alert('Error redeeming voucher: ' + error.message);
        }
    };
    
    

    return (
        <div className="container">
            <h2>Redeem Voucher</h2>
            <div>
                <input
                    type="text"
                    className="form-control"
                    placeholder="Enter Voucher Code"
                    value={voucherCode}
                    onChange={(e) => setVoucherCode(e.target.value)}
                />
            </div>
            <div>
                <DatePicker
                    selected={examDate}
                    onChange={(date) => { console.log(date);setExamDate(date)}}
                    showTimeSelect
                    //dateFormat="Pp"
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
