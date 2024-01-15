import React, { useState, useEffect, useContext } from 'react';
import AuthContext from '../../AuthContext'; // Adjust the import path as needed

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
                <td>{new Date(result.examDate).toLocaleDateString()}</td>
                <td>{result.scorePercentage}%</td>
                <td>{result.passed ? 'Passed' : 'Failed'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ExamHistory;
