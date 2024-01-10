import React, { useState , useContext } from 'react';
import { Form, Button, Alert, Container, Row, Col, FormCheck } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import AuthContext from '../AuthContext'; 

const Login = () => {
  const [UserName, setUsername] = useState('');
  const [Password, setPassword] = useState('');
  const [rememberPassword, setRememberPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [errors, setErrors] = useState([]);
  const [showErrorAlert, setShowErrorAlert] = useState(false);
  const [showSuccessAlert, setShowSuccessAlert] = useState(false);

  const { setToken } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      handleLogin();
    }
  };

  const handleLogin = async () => {
    const newErrors = [];
    if (!UserName.trim()) {
      newErrors.push('Username is required.');
    }
    if (UserName.length < 4 && UserName.trim() !== String()) {
      newErrors.push('Username has to be at least 4 letters!');
    }
    if (!Password.trim()) {
      newErrors.push('Password is required.');
    }

    if (newErrors.length > 0) {
      setErrors(newErrors);
      setShowErrorAlert(true);
      setShowSuccessAlert(false);
    } else {
      try {
        setLoading(true);
        const response = await fetch('https://localhost:5888/api/Auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            UserName,
            Password,
          }),
        });

        if (response.ok) {
          const data = await response.json();
          const { token } = data;
          localStorage.setItem('token', token);
          setToken(token);
          setShowSuccessAlert(true);
          setShowErrorAlert(false);
          navigate('/');
        } else {
          const errorData = await response.json();
          throw new Error(errorData.error);
        }
      } catch (error) {
        setShowErrorAlert(true);
        setShowSuccessAlert(false);
        setErrors([error.message]);
      } finally {
        setLoading(false);
      }
    }
  };

  return (
    <Container className="mt-5">
      <Row className="justify-content-center">
        <Col md={6} className="text-center">
          <h2>Login</h2>
          {showErrorAlert && (
            <Alert variant="danger" onClose={() => setShowErrorAlert(false)} dismissible>
              <ul>
                {errors.map((error, index) => (
                  <li key={index}>{error}</li>
                ))}
              </ul>
            </Alert>
          )}

          {showSuccessAlert && (
            <Alert variant="success" onClose={() => setShowSuccessAlert(false)} dismissible>
              Successfully logged in!
            </Alert>
          )}

          <Form className="d-flex row justify-content-center align-items-center">
          <Form.Group className="mb-2" controlId="formUsername">
              <Form.Label>Username</Form.Label>
              <Form.Control
                type="text"
                placeholder="Enter your UserName"
                value={UserName}
                onChange={(e) => setUsername(e.target.value)}
                onKeyDown={(e) => handleKeyPress(e)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-2" controlId="formPassword">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                placeholder="Enter your password"
                value={Password}
                onChange={(e) => setPassword(e.target.value)}
                onKeyDown={(e) => handleKeyPress(e)}
                required
              />
            </Form.Group>

            <Form.Group className="mb-2 d-flex row justify-content-center align-items-center" controlId="formRememberPassword">
              <FormCheck
                className='w-fit-content'
                type="checkbox"
                label="Remember Password"
                checked={rememberPassword}
                onChange={() => setRememberPassword(!rememberPassword)}
              />
            </Form.Group>
            <Link to="/register">Register</Link>
            <Button className="mt-3" variant="primary" onClick={handleLogin} disabled={loading}>
              {loading ? 'Logging in...' : 'Login'}
            </Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
};

export default Login;
