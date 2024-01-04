import React from 'react';

const LogoutButton = ({ handleLogout }) => {
    return (
      <a href="/" className="nav-link" onClick={handleLogout}>Logout</a>
    );
  };
  
export default LogoutButton;