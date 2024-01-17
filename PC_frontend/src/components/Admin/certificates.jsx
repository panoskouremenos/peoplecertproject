import React, { useState, useEffect, useContext } from 'react';
import AuthContext from '../../AuthContext'; // Adjust the import path as needed
import { Modal, Button } from 'react-bootstrap';

const CertificatesList = () => {
  const { token } = useContext(AuthContext);
  const [certificates, setCertificates] = useState([]);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [editCertificate, setEditCertificate] = useState({});
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [deleteCertificateId, setDeleteCertificateId] = useState(null);



  const handleEditClick = (certificate) => {
    setEditCertificate(certificate);
    setShowModal(true);
  };

  const handleConfirmDelete = async () => {
    try {
      const response = await fetch(`https://localhost:5888/api/Certificates/${deleteCertificateId}`, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${token}`,
        },
      });
      if (!response.ok) {
        throw new Error('Failed to delete certificate');
      }
      await fetchCertificates(); // Refresh the list
      setShowDeleteModal(false);
    } catch (error) {
      console.error('Error deleting certificate:', error);
    }
  };

  const handleDeleteClick = (certificateId) => {
    setDeleteCertificateId(certificateId);
    setShowDeleteModal(true);
  };

  const handleEditSave = async () => {
    const updatedCertificate = {
      title: editCertificate.title,
      assessmentTestCode: editCertificate.assessmentTestCode,
      minimumScore: parseInt(editCertificate.minimumScore, 10),
      maximumScore: parseInt(editCertificate.maximumScore, 10),
    };


  
    try {
      const response = await fetch(`https://localhost:5888/api/Certificates/${editCertificate.certificateId}`, {
        method: 'PUT',
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify(updatedCertificate),
      });
  
      if (!response.ok) {
        throw new Error('Failed to update certificate');
      }
      // Fetch the updated list of certificates
      await fetchCertificates();
      setShowModal(false);
    } catch (error) {
      console.error('Error updating certificate:', error);
    }
  };
  
  
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
        throw new Error('Failed to fetch certificates');
      }
      const data = await response.json();
      setCertificates(data);
    } catch (error) {
      console.error('Error fetching certificates:', error);
    }
  };

  useEffect(() => {
    fetchCertificates();
  }, [token]); // Dependency array includes token
  

  useEffect(() => {
    const fetchCertificates = async () => {
      setLoading(true);
      try {
        const response = await fetch('https://localhost:5888/api/Certificates', {
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

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <div className="container p-5">
      <h2>Certificates List</h2>
      <div className="table-responsive">
        <table className="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Title</th>
              <th>Assessment Test Code</th>
              <th>Minimum Score</th>
              <th>Maximum Score</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
  {certificates.map(certificate => (
    <tr key={certificate.certificateId}>
      <td>{certificate.certificateId}</td>
      <td>{certificate.title}</td>
      <td>{certificate.assessmentTestCode}</td>
      <td>{certificate.minimumScore}</td>
      <td>{certificate.maximumScore}</td>
      <td>
        <button 
          className="btn btn-primary p-2"
          style={{ width: 'max-content'}}
          onClick={() => handleEditClick(certificate)}
        >
          Quick Edit
        </button>
        
      </td>
      <td>        <button 
          className="btn btn-danger p-2"
          onClick={() => handleDeleteClick(certificate)}
        >
          Delete
        </button></td>
    </tr>
  ))}
</tbody>

        </table>
        <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Edit Certificate</Modal.Title>
        </Modal.Header>
        <Modal.Body>
  <label>New Certificate Title</label>
  <input
    type="text"
    className="form-control mb-2"
    placeholder="Title"
    value={editCertificate.title || ''}
    onChange={(e) => setEditCertificate({ ...editCertificate, title: e.target.value })}
  />
  <label>New Test Code</label>
  <input
    type="text"
    className="form-control mb-2"
    placeholder="TestCode"
    value={editCertificate.assessmentTestCode || ''}
    onChange={(e) => setEditCertificate({ ...editCertificate, assessmentTestCode: e.target.value })}
  />
  <label>New Minimum Score</label>
  <input
    type="number"
    className="form-control mb-2"
    placeholder="MinScore"
    value={editCertificate.minimumScore || ''}
    onChange={(e) => setEditCertificate({ ...editCertificate, minimumScore: e.target.value })}
  />
  <label>New Maximum Score</label>
  <input
    type="number"
    className="form-control mb-2"
    placeholder="MaxScore"
    value={editCertificate.maximumScore || ''}
    onChange={(e) => setEditCertificate({ ...editCertificate, maximumScore: e.target.value })}
  />
  </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleEditSave}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
      <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
  <Modal.Header closeButton>
    <Modal.Title>Delete Certificate</Modal.Title>
  </Modal.Header>
  <Modal.Body>
    Are you sure you want to delete this certificate?
  </Modal.Body>
  <Modal.Footer>
    <Button variant="secondary" onClick={() => setShowDeleteModal(false)}>
      No
    </Button>
    <Button variant="danger" onClick={handleConfirmDelete}>
      Yes, Delete
    </Button>
  </Modal.Footer>
</Modal>

      </div>
    </div>
  );
};

export default CertificatesList;
