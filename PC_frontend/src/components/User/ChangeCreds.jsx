import React, { useState, useContext } from 'react';
import AuthContext from '../../AuthContext'; // Adjust the import path as needed

const ChangeCreds = ({ updateUsername }) => {
  const { token } = useContext(AuthContext); // Assuming you have AuthContext set up for token

  const [username, setUsername] = useState('');
  const [repeatUsername, setRepeatUsername] = useState('');

  const [password, setPassword] = useState('');
  const [repeatPassword, setRepeatPassword] = useState('');

  const [message, setMessage] = useState('');

  const handleUsernameChange = async () => {
    if (username !== repeatUsername) {
      setMessage('Usernames do not match.');
      return;
    }

    try {
      const requestBody = { newUsername: username };
      const response = await fetch('https://localhost:5888/api/Auth/change-username', {
        method: 'PUT',
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify(requestBody),
      });
      if (!response.ok) {
        throw new Error('Failed to change username');
      }
      updateUsername(username); 
      setMessage('Username successfully changed.');
    } catch (error) {
      console.error('Error changing username:', error);
      setMessage('Error changing username. Probably username already exists.');
    }
  };

  const handlePasswordChange = async () => {
    if (password !== repeatPassword) {
      setMessage('Passwords do not match.');
      return;
    }

    try {
      const requestBody = { newPassword: password };
      const response = await fetch('https://localhost:5888/api/Auth/change-password', {
        method: 'PUT',
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify(requestBody),
      });
      if (!response.ok) {
        throw new Error('Failed to change password');
      }
      setMessage('Password successfully changed.');
    } catch (error) {
      console.error('Error changing password:', error);
      setMessage('Error changing password.');
    }
  };

  return (
    <div className="container p-5">
      <div className="row">
        <div className="col-md-6">
          <h3>Change Username</h3>
          <input
            type="text"
            className="form-control mb-2"
            placeholder="Type username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <input
            type="text"
            className="form-control mb-2"
            placeholder="Repeat username"
            value={repeatUsername}
            onChange={(e) => setRepeatUsername(e.target.value)}
          />
          <button className="btn btn-primary" onClick={handleUsernameChange}>Change Username</button>
        </div>

        <div className="col-md-6">
          <h3>Change Password</h3>
          <input
        type="password"
        className="form-control mb-2"
        placeholder="Type password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <input
        type="password"
        className="form-control mb-2"
        placeholder="Repeat password"
        value={repeatPassword}
        onChange={(e) => setRepeatPassword(e.target.value)}
      />
      <button className="btn btn-primary" onClick={handlePasswordChange}>Change Password</button>
    </div>
  </div>

  {message && (
    <div className="alert alert-info mt-3">
      {message}
    </div>
  )}
</div>
);
};
export default ChangeCreds;