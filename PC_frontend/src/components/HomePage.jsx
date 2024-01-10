import React, { useContext } from 'react';
import AuthContext from '../AuthContext';

const HomePage = () => {
  const { user } = useContext(AuthContext);
  
  return (
    <div>
      {user ? (
        <p>Hello, <b>{user.username}</b>!</p>
      ) : (
        <p>You are not logged in.</p>
      )}
    </div>
  );
};

export default HomePage;
