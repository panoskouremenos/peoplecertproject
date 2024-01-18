import React, { useState, useEffect, useContext } from 'react';
import AuthContext from '../../AuthContext'; 
import html2pdf from 'html2pdf.js';

const ExamHistory = () => {
  const { token } = useContext(AuthContext);
  const [examResults, setExamResults] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchExamResults = async () => {
      setLoading(true);
      try {
        const response = await fetch('https://localhost:5888/api/ExamResults/MyExamResults', {
          method: 'GET',
          headers: {
            "Content-Type": "application/json",
            'Authorization': `Bearer ${token}`,
          },
        });

        if (!response.ok) {
          throw new Error('Network response was not ok');
        }

        const data = await response.json();
        setExamResults(data);
      } catch (error) {
        console.error('Error fetching exam results:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchExamResults();
  }, [token]);

  const fetchAndGeneratePDF = async (certificateId) => {
    try {
      const response = await fetch(`https://localhost:5888/api/CertificateTopicMarks/GetExamReport/${certificateId}`, {
        method: 'GET',
        headers: {
          "Content-Type": "application/json",
          'Authorization': `Bearer ${token}`,
        },
      });
      console.log(response.status);
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
  
      const examReport = await response.json();
      generatePDF(examReport);
    } catch (error) {
      console.error('Error fetching exam report:', error);
    }
  };

  
  const generatePDF = (examReport) => {
    const pdfContent = document.createElement("div");
    const certificateTitle = examReport[0].certificateTitle;
    pdfContent.innerHTML = `<h3>${certificateTitle} Exam Report</h3>`;
    pdfContent.innerHTML += `
      <table>
        <tr>
          <th>Topic</th>
          <th>Max Awarded Marks</th>
          <th>Awarded Marks</th>
        </tr>
        ${examReport.map(item => `
          <tr>
            <td>${item.topic}</td>
            <td>${item.maxAwardedMarks}</td>
            <td>${item.awardedMarks}</td>
          </tr>`).join("")}
      </table>`;
  
    html2pdf().from(pdfContent).toPdf().save(`${certificateTitle}_exam_report.pdf`);
  };

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <div className="container p-5">
      <h2>Exam History</h2>
      <div className="table-responsive">
        <table className="table">
          <thead>
            <tr>
              <th>Certificate Title</th>
              <th>Exam Date</th>
              <th>Score (%)</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {examResults.map(result => (
              <tr key={`${result.certificateTitle}-${result.examDate}`}>
                <td>{result.certificateTitle}</td>
                {console.log(result)}
                <td>{new Date(result.examDate).toLocaleDateString()}</td>
                <td>{result.scorePercentage}%</td>
                <td>{result.passed ? 'Passed' : 'Failed'}</td>
                <td>
                  {/*<button className="btn btn-primary" onClick={() => fetchAndGeneratePDF(result.certificateId)}>
                    Generate Report
                  </button>*/}
                </td>
              </tr>
            ))}
          </tbody>

        </table>
      </div>
    </div>
  );
};

export default ExamHistory;
