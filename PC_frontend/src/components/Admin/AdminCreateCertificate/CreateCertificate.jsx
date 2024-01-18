import { Form, Row, Col } from "react-bootstrap";

const CreateCertificate = ({ certificateInfo, setCertificateInfo }) => {
  // todo - validate the values

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setCertificateInfo((prev) => {
      return { ...prev, [name]: value };
    });
  };

  return (
    <div className="card m-2 p-3 border ">
      <h2> Create Certificate</h2>
      <Form.Group as={Row} className="mb-3">
        <Form.Label column sm="3">
          Certificate:
        </Form.Label>
        <Col sm="9 mb-1">
          <Form.Control
            type="text"
            name="title"
            value={certificateInfo?.title}
            onChange={handleInputChange}
          />
        </Col>
        <Form.Label column sm="3">
          Assessment Test Code:
        </Form.Label>
        <Col sm="9 mb-1">
          <Form.Control
            type="text"
            name="assessmentTestCode"
            value={certificateInfo?.assessmentTestCode}
            onChange={handleInputChange}
          />
        </Col>
        <Form.Label column sm="3">
          Max Score:
        </Form.Label>
        <Col sm="9 mb-1">
          <Form.Control
            type="number"
            name="maximumScore"
            value={certificateInfo?.maximumScore}
            onChange={handleInputChange}
          />
        </Col>
        <Form.Label column sm="3">
          Min Score:
        </Form.Label>
        <Col sm="9 mb-1">
          <Form.Control
            type="number"
            name="minimumScore"
            value={certificateInfo?.minimumScore}
            onChange={handleInputChange}
          />
        </Col>
      </Form.Group>
    </div>
  );
};

export default CreateCertificate;
