import { useState } from "react";
import { Form, Col, Row } from "react-bootstrap";

const CheckBox = ({
  id,
  topicsIndex,
  questionsIndex,
  certificateInfo,
  setCertificateInfo,
}) => {
  const handleCheckboxChange = () => {
    setCertificateInfo((prev) => {
      const updatedCertificateTopicMarks = [...prev.certificateTopicMarks];
      const updatedQuestions = [
        ...prev.certificateTopicMarks[topicsIndex].questions,
      ];

      updatedQuestions[questionsIndex] = {
        ...updatedQuestions[questionsIndex],
        checkedBox: id,
      };

      updatedCertificateTopicMarks[topicsIndex] = {
        ...updatedCertificateTopicMarks[topicsIndex],
        questions: updatedQuestions,
      };

      return {
        ...prev,
        certificateTopicMarks: updatedCertificateTopicMarks,
      };
    });
  };

  const handleInputChange = (e) => {
    setCertificateInfo((prev) => {
      const updatedCertificateTopicMarks = [...prev.certificateTopicMarks];
      const updatedQuestions = [
        ...prev.certificateTopicMarks[topicsIndex].questions,
      ];

      updatedQuestions[questionsIndex] = {
        ...updatedQuestions[questionsIndex],
        [id]: e.target.value,
      };

      updatedCertificateTopicMarks[topicsIndex] = {
        ...updatedCertificateTopicMarks[topicsIndex],
        questions: updatedQuestions,
      };

      return {
        ...prev,
        certificateTopicMarks: updatedCertificateTopicMarks,
      };
    });
  };

  return (
    <Row className="m-1 p-1">
      <Col md={2} sm={3}>
        <Form.Check
          type="checkbox"
          id="checkboxId"
          label=""
          checked={
            certificateInfo.certificateTopicMarks[topicsIndex].questions[
              questionsIndex
            ].checkedBox === id
          }
          onChange={handleCheckboxChange}
        />
      </Col>
      <Col md={7} sm={9}>
        <Form.Control
          type="text"
          placeholder="Enter value"
          value={
            certificateInfo.certificateTopicMarks[topicsIndex].questions[
              questionsIndex
            ][id]
          }
          onChange={handleInputChange}
        />
      </Col>
    </Row>
  );
};

export default CheckBox;
