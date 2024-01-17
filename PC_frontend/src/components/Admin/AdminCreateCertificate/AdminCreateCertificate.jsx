import { useState, useContext } from "react";
import { Form, Button } from "react-bootstrap";
import CertificateTopics from "./CertificateTopics";
import CreateCertificate from "./CreateCertificate";
import AuthContext from "../../../AuthContext";

const initialCertificate = {
  title: "",
  assessmentTestCode: "",
  minimumScore: 0,
  maximumScore: 0,
  certificateTopicMarks: [
    {
      topicMarks: "",
      questions: [
        {
          certificateTopicMarksId: "",
          questionText: "",
          questionType: "",
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

const formatedCertificate = (certificate) => {
  if (!certificate) {
    return null;
  }

  const convertedObject = {
    "title": certificate.title || "",
    "assessmentTestCode": certificate.assessmentTestCode || "",
    "minimumScore": parseInt(certificate.minimumScore) || 0,
    "maximumScore": parseInt(certificate.maximumScore) || 0,
    "certificateTopicMarks": []
  };

  if (Array.isArray(certificate.certificateTopicMarks)) {
    convertedObject.certificateTopicMarks = certificate.certificateTopicMarks.map((topic) => {
      const convertedTopic = {
        "topicDesc": topic.topicMarks || "",
        "questions": []
      };

      if (Array.isArray(topic.questions)) {
        convertedTopic.questions = topic.questions.map((question) => {
          const possibleAnswers = Object.keys(question)
            .filter((key) => !isNaN(key)) 
            .map((key) => question[key])
            .join(", ");

          const convertedQuestion = {
            "certificateTopicMarksId": parseInt(question.certificateTopicMarksId) || 0,
            "questionText": question.questionText || "",
            "questionType": parseInt(question.questionType) || 0,
            "possibleAnswers": possibleAnswers || "",
            "answer": question[question.checkedBox] || ""
          };

          return convertedQuestion;
        });
      }

      return convertedTopic;
    });
  }

  return convertedObject;
};


const AdminCreateCertificate = () => {
  const [certificateInfo, setCertificateInfo] = useState(initialCertificate);
  const { token } = useContext(AuthContext);
  
  const handleCertificateCreation = async () => {
    try{
      const response = await fetch("https://localhost:5888/api/Certificates" , {
        method : "POST",
        headers: {
          'Authorization': `bearer ${token}`,
          'Content-Type': 'application/json',
        },
        body : JSON.stringify(formatedCertificate(certificateInfo))
      });

      if(response.ok){
        const result = await response.json();
         alert('ok');
      }else{
        alert('not ok');
      }
    }catch(error){
      alert(error);
    }finally{

    }
  }

  const submitHandler = () => {
    console.log(JSON.stringify(certificateInfo));
    /*
    {
    "title": "C# Fundamentals",
    "assessmentTestCode": "Hello world",
    "minimumScore": "55",
    "maximumScore": "80",
    "certificateTopicMarks": [{
        "topicMarks": "cancer",
        "questions": [{
            "0": "yes",
            "1": "yes",
            "2": "yes",
            "3": "yes",
            "certificateTopicMarksId": "",
            "questionText": "Do you have?",
            "questionType": "0",
            "checkedBox": 0
        }, {
            "0": "cancer",
            "1": "cancer",
            "2": "cancer",
            "3": "cancer",
            "certificateTopicMarksId": "",
            "questionText": "You dont have?",
            "questionType": "0",
            "checkedBox": 0
        }]
    }, {
        "topicMarks": "=)",
        "questions": [{
            "0": ">=D",
            "1": ">=(",
            "2": ">:/",
            "3": "<:/",
            "certificateTopicMarksId": "",
            "questionText": "=)(",
            "questionType": "0",
            "checkedBox": 0
        }, {
            "0": "vai",
            "1": "vai",
            "2": "vai",
            "3": "vai",
            "certificateTopicMarksId": "",
            "questionText": "da",
            "questionType": "0",
            "checkedBox": 0
        }]
    }]
}
*/
    handleCertificateCreation();
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
        <Button onClick={submitHandler}>Submit</Button>
      </Form>
    </>
  );
};

export default AdminCreateCertificate;
