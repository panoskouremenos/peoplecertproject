import React, { useContext, useState, useEffect } from 'react';
import AuthContext from '../AuthContext';

const HomePage = () => {
  const { user } = useContext(AuthContext);
  const [imageScale, setImageScale] = useState(58.08); 

  useEffect(() => {
    const scaleUpTimer = setTimeout(() => {
      setImageScale(100); 
    }, 1000);

    return () => {
      clearTimeout(scaleUpTimer);
    };
  }, []); 

  return (
    <div>
      {user ? (
        <div >
        <h4><b>{user.username}</b>, welcome to  <b>People<span style={{color: 'red', textAlign: 'center'}}>Cert </span></b>!</h4></div>
      ) : (
        <p style={{ color: 'black',fontSize: '30px', margin: '50px' }}>
          You are<span style={{color: 'red',textDecoration: 'underline'}}> not </span>logged in.Please
         login first!</p>
      )}
      <div
        className="container-fluid"
        style={{
          backgroundImage: 'url("src/Images/favicons/peoplecert-first-image.png")',
          backgroundSize: `${imageScale}%`,
          backgroundPosition: 'center', 
          backgroundRepeat: 'no-repeat',
          height: '60vh',
          width: '100vw', 
          margin: '30px',
          transition: 'background-image 1s ease, background-size 2s ease',
        }}
      ></div>
      <div style={{ display: 'flex', alignItems: 'center' }}>                                                                                                                                                                                                                                                                                                       <p
  style={{
    width: '327%',
    backgroundColor: 'red',
    textAlign: 'start',
    padding: '1%',
    backgroundPosition: 'left',
    backgroundRepeat: 'no-repeat',

    alignItems: 'center',
  }}
>
  <img
    src="src/Images/favicons/phone.png"
    alt="Favicon"
    style={{ marginRight: '10px', width: '20px', height: '25px' }}
  />
  +30 210 3729100
</p>
<br></br>
<p
  style={{
    width: '167%',
    backgroundColor: 'red',
    textAlign: 'start',
    padding: '1%',
    backgroundPosition: 'left',
    backgroundRepeat: 'no-repeat',
    alignItems: 'center', 
  }}
>
  <img
    src="src/Images/favicons/email.png" 
    alt="Favicon"
    style={{ marginRight: '10px', width: '20px', height: '25px' }}
  />
  info@peoplecert.org
</p>

</div>
      {/* <p style={{backgroundColor:'red', textAlign:'end', width: '167%',padding: '3%',
      backgroundPosition: 'left', // Center the background image
      backgroundRepeat: 'no-repeat', backgroundImage: 'url("src/Images/favicons/phone.png")'}}></p> */}
      {/* <p style={{backgroundColor:'black', textAlign:'end'}}>-</p>
      <p style={{backgroundColor:'red', textAlign:'end'}}>-</p> */}
    </div>
  );
};

export default HomePage;