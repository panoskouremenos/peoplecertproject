import { Form, Row, Col } from "react-bootstrap";
import CertificateQuestions from "./CertificateQuestions";

const CertificateTopic = ({ index, certificateInfo, setCertificateInfo }) => {
  const onChangeHandler = (e) => {
    const { name, value } = e.target;

    const updatedCertificateTopicMarks = [
      ...certificateInfo.certificateTopicMarks,
    ];
    updatedCertificateTopicMarks[index][name] = value;

    setCertificateInfo((prev) => {
      return {
        ...prev,
        certificateTopicMarks: updatedCertificateTopicMarks,
      };
    });
  };

  return (
    <>
      <h3>Topic Info</h3>
      <Form.Group as={Row} className="mb-3">
        <Form.Label column sm="3">
          Certificate Topic Marks:
        </Form.Label>
        <Col sm="9">
          <Form.Control
            type="text"
            name="topicMarks"
            value={certificateInfo.certificateTopicMarks[index].topicMarks}
            onChange={onChangeHandler}
          />
        </Col>
        {/* <Form.Label column sm="3">
          Description:
        </Form.Label>
        <Col sm="9">
          <Form.Control
            as="textarea"
            rows={5}
            name="topicDesc"
            value={certificateInfo.certificateTopicMarks[index].topicDesc}
            onChange={onChangeHandler}
          />
        </Col> */}
      </Form.Group>
      <CertificateQuestions
        index={index}
        certificateInfo={certificateInfo}
        setCertificateInfo={setCertificateInfo}
      />
    </>
  );
};

export default CertificateTopic;
