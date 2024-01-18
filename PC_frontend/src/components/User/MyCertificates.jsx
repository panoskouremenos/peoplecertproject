import React, { useState, useEffect, useContext } from 'react';
import html2pdf from 'html2pdf.js';
import AuthContext from '../../AuthContext';

const formatDate = (dateString) => {
    if (!dateString) return '';
    const date = new Date(dateString);
    return `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear().toString().substring(2)}`;
  };

const CertificateGrid = () => {
  const { token } = useContext(AuthContext);
  const [certificates, setCertificates] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchCertificates = async () => {
      setLoading(true);
      try {
        const response = await fetch('https://localhost:5888/api/ExamResults/UserCertificates', {
          method: 'GET',
          headers: {
            "Content-Type": "application/json",
            'Authorization': `Bearer ${token}`,
          },
        });

        if (!response.ok) {
          throw new Error('Network response was not ok');
        }

        const data = await response.json();
        setCertificates(data);
      } catch (error) {
        console.error('Error fetching certificates:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchCertificates();
  }, [token]);

  const downloadCertificate = (certificate) => {
    const content = `
      <div style="padding: 30px; background: #f2f2f2; color: #333; text-align: center; border: 5px solid #ddd;">
        <h1 style="color: #007bff;">PeopleCert</h1>
        <h2 style="color: #28a745;">Certificate of Achievement</h2>
        <h3 style="font-weight: normal;">This is to certify that</h3>
        <h2 style="color: #17a2b8;">${certificate.firstName} ${certificate.lastName}</h2>
        <p style="font-size: 20px;">has successfully completed the</p>
        <h3 style="color: #dc3545;">${certificate.certificateTitle}</h3>
        <p style="font-size: 18px;">with a score of <strong>${certificate.scorePercentage}%</strong></p>
        <p style="font-size: 18px; border-top: 1px solid #ccc; padding-top: 10px;">Date: ${new Date(certificate.examDate).toLocaleDateString()}</p>
      </div>
    `;
  
    const options = {
      margin: [10, 0], 
      filename: `${certificate.certificateTitle}.pdf`,
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 2 },
      jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
    };
  
    html2pdf().from(content).set(options).save();
  };

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <div className="container">
      <h2>Your Certificates</h2>
      <div className="row">
        {certificates.map((certificate, index) => (
          <div key={index} className="col-md-4 mb-4">
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{certificate.certificateTitle}</h5>
                <p className="card-text">Score: {certificate.scorePercentage}%</p>
                <p className="card-text">Date obtained: {formatDate(certificate.examDate)}</p>
                <button onClick={() => downloadCertificate(certificate)} className="btn btn-primary">Download Certificate</button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CertificateGrid;
