import CertificateQuestion from "./CertificateQuestion";
import { Button } from "react-bootstrap";

const initialQuestion = {
  certificateTopicMarksId: "",
  questionText: "",
  questionType: "",
  // possibleAnswers: "",
  0: "",
  1: "",
  2: "",
  3: "",
  checkedBox: "",
};

const CertificateQuestions = ({
  index,
  certificateInfo,
  setCertificateInfo,
}) => {
  const addQuestionHandler = () => {
    const updatedQuestions = [
      ...certificateInfo.certificateTopicMarks[index].questions,
      initialQuestion,
    ];
    setCertificateInfo((prev) => {
      const updatedCertificateTopicMarks = [...prev.certificateTopicMarks];
      updatedCertificateTopicMarks[index] = {
        ...updatedCertificateTopicMarks[index],
        questions: updatedQuestions,
      };

      return {
        ...prev,
        certificateTopicMarks: updatedCertificateTopicMarks,
      };
    });
  };

  return (
    <div className="card m-2 p-3 border ">
      <h3> Create Question</h3>
      <Button onClick={addQuestionHandler}>Add Question</Button>
      <br />
      {certificateInfo.certificateTopicMarks[index].questions.map(
        (question, i) => {
          return (
            <CertificateQuestion
              key={i}
              topicsIndex={index}
              questionsIndex={i}
              certificateInfo={certificateInfo}
              setCertificateInfo={setCertificateInfo}
            />
          );
        }
      )}
    </div>
  );
};

export default CertificateQuestions;
