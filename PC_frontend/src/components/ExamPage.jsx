import React, { useState, useEffect , useContext } from 'react';
import AuthContext from '../AuthContext';
import ExamContext from '../ExamContext';

const ExamPage = () => {
  const [questions, setQuestions] = useState([]);
  const [activeQuestion , setActiveQuestion] = useState(0);
  const { token } = useContext(AuthContext);
  const [isLoading, setIsLoading] = useState(true);

  function convertNumberToLetter(number) {
    if (number >= 0 && number <= 25) {
      return String.fromCharCode(65 + number);
    } else {
      return null;
    }
  }
  const handleRadioClick = (index) => {
    setQuestions((prevQuestions) => {
      const updatedQuestions = [...prevQuestions];
      updatedQuestions[activeQuestion].checkedAnswer = index;
      return updatedQuestions;
    });
  };
  
  const handleChangeQuestion = (index) => {
    setActiveQuestion(index);
  }
  const fetchExamQuestions = async (id) => {
    try {
      const response = await fetch(`https://localhost:5888/api/Certificates/${id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
      });

      if (response.ok) {
        const result = await response.json();
        const extractedQuestions = result.certificateTopicMarks.flatMap((topic) =>
          topic.questions.map((question) => ({
            topicDesc: topic.topicDesc,
            questionId: question.questionId,
            questionText: question.questionText,
            possibleAnswers: question.possibleAnswers,
            checkedAnswer : false
          }))
        );
        setQuestions(extractedQuestions);
      } else {
        alert("Failed to fetch exam questions");
      }
    } catch (error) {
      alert(error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchExamQuestions(11);
  }, [token]);

  return (
    <div id="exampage" className="container mt-5">
      <div className="row">
      {isLoading ? (
              <p>Loading...</p>
            ) : (
              <>
        <div className="col-md-3">
          <div className="list-group" style={{overflowY : "scroll" , maxHeight : "80%" , padding : "1em" }}>
            {questions.map((q , i) => {
                  return (
                    //questions[activeQuestion].checkedAnswer === false
                    <button onClick={(e) => handleChangeQuestion(i)} key={i} type="button" className={`${questions[activeQuestion].checkedAnswer === false ? "isnotchecked" : "ischecked"} list-group-item list-group-item-action ${activeQuestion === i ? "active" : ""}`}>
                    Question {i}
                  </button>
                  );
              })}
          </div>
        </div>
        <div className="col-md-9 d-flex flex-column align-items-center">
          <div className="border p-3">
            <h4>MCQ Quiz</h4>
            <div className="d-flex flex-row align-items-center question-title">
              <h3 className="text-danger">{questions[activeQuestion].questionText}</h3>
            </div>
            <div className="ans ml-2">
            {questions[activeQuestion].possibleAnswers.split(',').map((_ , i) => {
              return (
                <label className="options">{convertNumberToLetter(i)} : {questions[activeQuestion].possibleAnswers.split(',')[i]}
                 <input type="radio" name="radio" onClick={(e) => handleRadioClick(i)} checked={questions[activeQuestion].checkedAnswer === false ? "" : questions[activeQuestion].checkedAnswer === i}/>
                  <span className="checkmark"></span>
                </label>)
            })
            }
            </div>
            <div className="d-flex flex-row justify-content-between align-items-center p-3 bg-white">
              {activeQuestion > 0 && 
              <button onClick={(e) => setActiveQuestion(activeQuestion > 0 ? activeQuestion-1 : 0)} className="btn btn-primary d-flex align-items-center btn-danger" type="button">
                <i className="fa fa-angle-left mt-1 mr-1"></i>&nbsp;previous
              </button>
              }
              {activeQuestion < questions.length-1 &&
              <button onClick={(e) => setActiveQuestion(activeQuestion < questions.length ? activeQuestion+1 : 0)} className="btn btn-primary border-success align-items-center btn-success" type="button">
                Next<i className="fa fa-angle-right ml-2"></i>
              </button>
              }
            </div>
          </div>
          <div className="endexam mt-3"><button className="btn btn-danger">End Exam</button></div>
        </div>
              </>
            )}
      </div>
    </div>
  );
};

export default ExamPage;
