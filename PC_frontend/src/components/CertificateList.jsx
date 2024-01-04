import React from 'react';
import Certificate from './Certificate';

const CertificateList = ({ certificates, onDelete, onEdit }) => {
  return (
    <div>
      {certificates.map(certificate => (
        <Certificate key={certificate.id} certificate={certificate} onDelete={onDelete} onEdit={onEdit} />
      ))}
    </div>
  );
};

export default CertificateList;