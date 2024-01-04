import React, { useState } from "react";
import { Form, Button, Alert, Container, Row, Col } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const RegisterPage = () => {
  const [formData, setFormData] = useState({
    username: "",
    password: "",
    reEnterPassword: "",
  });
  const [errors, setErrors] = useState([]);
  const [showErrorAlert, setShowErrorAlert] = useState(false);
  const [showSuccessAlert, setShowSuccessAlert] = useState(false);
  const navigate = useNavigate();
  const handleRegistration = async () => {
    try {
      const response = await fetch("http://localhost:3001/api/register", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username: formData.username,
          password: formData.password,
        }),
      });

      if (response.ok) {
        setShowSuccessAlert(true);
        setShowErrorAlert(false);
        setFormData({ username: "", password: "", reEnterPassword: "" });
        setTimeout(() => {
          navigate('/login');
        },500)
      } else {
        setErrors(['Username already taken.']);
        setShowSuccessAlert(false);
        setShowErrorAlert(true);
      }
    } catch (error) {
      console.error('Error registering user:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      handleRegister();
    }
  };

  const handleRegister = () => {
    const newErrors = [];
    if (!formData.username.trim()) {
      newErrors.push('Username is required.');
    }
    if (formData.username.length < 4) {
      newErrors.push('Username has to be at least 4 letters!');
    }
    if (!formData.password.trim()) {
      newErrors.push('Password is required.');
    }
    if (!formData.reEnterPassword.trim()) {
      newErrors.push('Password validation is required.');
    }
    if (formData.password !== formData.reEnterPassword) {
      newErrors.push('Passwords provided are not identical.');
    }

    setErrors(newErrors);

    if (newErrors.length === 0) {
      handleRegistration();
    } else {
      setShowErrorAlert(true);
      setShowSuccessAlert(false);
    }
  };

  return (
    <Container className="mt-5">
      <Row className="justify-content-center">
        <Col md={6} className="text-center">
          <h2>Register</h2>
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
              Successfully Registered!
            </Alert>
          )}
          <Form className="d-flex row justify-content-center align-items-center">
            <Form.Group controlId="formUsername">
              <Form.Label>Username:</Form.Label>
              <Form.Control
                type="text"
                name="username"
                value={formData.username}
                placeholder="Enter your Username"
                onChange={handleChange}
                onKeyDown={handleKeyPress}
                required
              />
            </Form.Group>

            <Form.Group controlId="formPassword">
              <Form.Label>Password:</Form.Label>
              <Form.Control
                type="password"
                name="password"
                value={formData.password}
                placeholder="Enter your Password"
                onChange={handleChange}
                onKeyDown={handleKeyPress}
                required
              />
            </Form.Group>

            <Form.Group controlId="formReEnterPassword">
              <Form.Label>Re-enter Password:</Form.Label>
              <Form.Control
                type="password"
                name="reEnterPassword"
                value={formData.reEnterPassword}
                placeholder="Enter your Password again"
                onChange={handleChange}
                onKeyDown={handleKeyPress}
                required
              />
            </Form.Group>

            <Button className="mt-3" variant="primary" onClick={handleRegister}>
              Register
            </Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
};

export default RegisterPage;
