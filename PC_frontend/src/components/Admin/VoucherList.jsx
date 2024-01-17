import React, { useState, useEffect, useContext } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Modal, Button, Table } from 'react-bootstrap';
import AuthContext from '../../AuthContext';

const VoucherList = () => {
    const { id } = useParams();
    const [vouchers, setVouchers] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [certificates, setCertificates] = useState([]);
    const [selectedCertificateId, setSelectedCertificateId] = useState('');
    const [newVoucherCode, setNewVoucherCode] = useState('');
    const { token } = useContext(AuthContext);
    const [loading, setLoading] = useState(false);

    // Fetch vouchers
        const fetchVouchers = async () => {
            try {
                const response = await fetch('https://localhost:5888/api/ExamVouchers', {
                    method: 'GET',
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': `Bearer ${token}`,
                    },
                });
                const data = await response.json();
                setVouchers(data)
            } catch (error) {
                console.error('Error fetching candidate:', error);
            } finally {
                setLoading(false);
            }
        };

        if (token) {
            fetchVouchers();
        }


        const fetchCertificates = async () => {
            try {
                const response = await fetch('https://localhost:5888/api/Certificates', {
                    method: 'GET',
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': `Bearer ${token}`,
                    },
                });

                if (!response.ok) {
                    console.log(response.status)
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setCertificates(data)
            } catch (error) {
                console.error('Error fetching candidate:', error);
            } finally {
                setLoading(false);
            }
        };

        if (token) {
            fetchCertificates();
        }




    // Create new voucher
    const handleCreateVoucher = async () => {
        try {
            const response = await fetch('https://localhost:5888/api/ExamVouchers', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify({ CertificateId: selectedCertificateId })
            });
            const data = await response.json();
            setNewVoucherCode(data.voucherCode);
            fetchVouchers();
            setShowModal(false);
        } catch (error) {
            console.error('Error fetching vouchers:', error);
        }
    };


    useEffect(() => {
        if (token) {
            fetchVouchers();
            fetchCertificates();
        }
    }, [token]);

    if (loading) {
        return <p>Loading...</p>;
    }

    return (
        <div className="container">
            <Button onClick={() => setShowModal(true)}>Create new Voucher</Button>

            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Certificate</th>
                        <th>Used</th>
                        <th>Code</th>
                        <th>User</th>
                    </tr>
                </thead>
                <tbody>
                    {vouchers.map(voucher => (
                        <tr key={voucher.voucherID}>
                            <td>{voucher.voucherID}</td>
                            <td>{voucher.certificateTitle}</td>
                            <td>{voucher.isUsed ? 'Yes' : 'No'}</td>
                            <td>{voucher.voucherCode}</td>
                            <td>{voucher.userName === 'admin' ? 'None' : voucher.userName}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Create New Voucher</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <select
                        className="form-control"
                        value={selectedCertificateId}
                        onChange={(e) => setSelectedCertificateId(e.target.value)}
                    >
                        {certificates.map(certificate => (
                            <option key={certificate.certificateId} value={certificate.certificateId}>
                                {certificate.title}
                            </option>
                        ))}
                    </select>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowModal(false)}>
                        Cancel
                    </Button>
                    <Button variant="primary" onClick={handleCreateVoucher}>
                        Confirm
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default VoucherList;
