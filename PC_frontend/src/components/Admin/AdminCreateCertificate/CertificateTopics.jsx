import CertificateTopic from "./CertificateTopic";
import { Button } from "react-bootstrap";

const initialTopic = {
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
};

const CertificateTopics = ({ certificateInfo, setCertificateInfo }) => {
  const addTopicHandler = () => {
    setCertificateInfo((prev) => {
      return {
        ...prev,
        certificateTopicMarks: [...prev.certificateTopicMarks, initialTopic],
      };
    });
  };

  return (
    <div className="card m-2 p-3 border ">
      <h2> Create Topic</h2>
      <Button onClick={addTopicHandler}>Add Topic</Button>
      <br />
      {certificateInfo.certificateTopicMarks.map((topic, index) => {
        return (
          <CertificateTopic
            key={index}
            index={index}
            certificateInfo={certificateInfo}
            setCertificateInfo={setCertificateInfo}
          />
        );
      })}
    </div>
  );
};

export default CertificateTopics;
