import { useState } from "react";
import { Form, Button } from "react-bootstrap";
import CertificateTopics from "./CertificateTopics";
import CreateCertificate from "./CreateCertificate";

const initialCertificate = {
  title: "",
  assessmentTestCode: "",
  minimumScore: 0,
  maximumScore: 0,
  certificateTopicMarks: [
    {
      // topicDesc: "",
      topicMarks: "",
      questions: [
        {
          certificateTopicMarksId: "",
          questionText: "",
          questionType: "",
          // possibleAnswers: "",
          0: "",
          1: "",
          2: "",
          3: "",
          checkedBox: "",
        },
      ],
    },
  ],
};

const AdminCreateCertificate = () => {
  const [certificateInfo, setCertificateInfo] = useState(initialCertificate);

  const submitHandler = () => {
    console.log("certificateInfo", certificateInfo);
    // transform the data stracture based backend's needs
    // iterate the certificateTopicMarks and then the question of each topic
    // and transform each question based that
    // {
    //   "certificateTopicMarksId": 0,
    //   "questionText": "string",
    //   "questionType": 0,
    //   "possibleAnswers": "string",
    //   "answer": "string"
    // }
    // you can earch in the question for the attribut that has key the value of checkedBox
    // and then asign the value of that attribut to the answer
    //
    //
    // Make the request to the backend
  };

  return (
    <>
      <div>AdminCreateCertificate</div>
      <br />
      <Form>
        <CreateCertificate
          certificateInfo={certificateInfo}
          setCertificateInfo={setCertificateInfo}
        />
        <CertificateTopics
          certificateInfo={certificateInfo}
          setCertificateInfo={setCertificateInfo}
        />
        {/* <CertificateQuestion /> */}
        <Button onClick={submitHandler}>Submit</Button>
      </Form>
    </>
  );
};

export default AdminCreateCertificate;
