import { useState } from "react";
import { Form, Row, Col } from "react-bootstrap";
import CheckBox from "../common/CheckBox";

const CertificateQuestion = ({
  topicsIndex,
  questionsIndex,
  certificateInfo,
  setCertificateInfo,
}) => {
  const onChangeHandler = (e) => {
    const { name, value } = e.target;
    setCertificateInfo((prev) => {
      const updatedCertificateTopicMarks = [...prev.certificateTopicMarks];
      const updatedQuestions = [
        ...prev.certificateTopicMarks[topicsIndex].questions,
      ];

      updatedQuestions[questionsIndex] = {
        ...updatedQuestions[questionsIndex],
        [name]: value,
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
    <Form.Group as={Row} className="mb-3">
      <Form.Label column sm="3">
        Question Text:
      </Form.Label>
      <Col sm="9">
        <Form.Control
          type="text"
          name="questionText"
          value={
            certificateInfo.certificateTopicMarks[topicsIndex].questions[
              questionsIndex
            ].questionText
          }
          onChange={onChangeHandler}
        />
      </Col>
      <Form.Label column sm="3">
        Type:
      </Form.Label>
      <Col sm="9">
        <Form.Select
          aria-label="Default select example"
          name="questionType"
          value={
            certificateInfo.certificateTopicMarks[topicsIndex].questions[
              questionsIndex
            ].questionType
          }
          onChange={onChangeHandler}
        >
          <option>Select Type</option>
          <option value="0">0</option>
          <option value="1">1</option>
        </Form.Select>
      </Col>
      <Col sm="9">
        <CheckBox
          id={0}
          topicsIndex={topicsIndex}
          questionsIndex={questionsIndex}
          certificateInfo={certificateInfo}
          setCertificateInfo={setCertificateInfo}
        />
        <CheckBox
          id={1}
          topicsIndex={topicsIndex}
          questionsIndex={questionsIndex}
          certificateInfo={certificateInfo}
          setCertificateInfo={setCertificateInfo}
        />
        <CheckBox
          id={2}
          topicsIndex={topicsIndex}
          questionsIndex={questionsIndex}
          certificateInfo={certificateInfo}
          setCertificateInfo={setCertificateInfo}
        />
        <CheckBox
          id={3}
          topicsIndex={topicsIndex}
          questionsIndex={questionsIndex}
          certificateInfo={certificateInfo}
          setCertificateInfo={setCertificateInfo}
        />
      </Col>
    </Form.Group>
  );
};

export default CertificateQuestion;
