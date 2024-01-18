import React from 'react';
import { useNavigate } from 'react-router-dom';

const AdminDashboard = () => {
  const navigate = useNavigate();

  return (
    <div className="container mt-5">
      <h1 className="text-center mb-4">Admin Dashboard</h1>
      
      <div className="row">
        <div className="col-md-6">
          <h2>Candidates Operations</h2>
          <button className="btn btn-info mb-2" onClick={() => navigate('/admin/candidates')}>View All Candidates</button>
        </div>
        <div className="col-md-6">
          <h2>User Operation</h2>
          <button className="btn btn-info mb-2" onClick={() => navigate('/admin/userlist')}>View all users</button>
        </div>

        <div className="col-md-6">
          <h2>Certificates Operations</h2>
          <button className="btn btn-success m-1" onClick={() => navigate('/admin/createcert')}>Create Certificate</button>
          <button className="btn btn-secondary m-1" onClick={() => navigate('/admin/certificates')}>View All Certificates</button>
          <button className="btn btn-secondary m-1" onClick={() => navigate('/admin/vouchers')}>Generate Voucher</button>
        </div>
      </div>
    </div>
  );
};

export default AdminDashboard;
