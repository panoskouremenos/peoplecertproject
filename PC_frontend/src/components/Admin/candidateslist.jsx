import React, { useState, useEffect, useContext } from 'react';
import AuthContext from "../../AuthContext";
import { Link } from 'react-router-dom'; // Import Link

const CandidatesList = () => {
  const { token } = useContext(AuthContext);
  const [candidates, setCandidates] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchCandidates = async () => {
      try {
        setLoading(true);
        const response = await fetch('https://localhost:5888/api/Candidates', {
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
        setCandidates(data);
      } catch (error) {
        console.error('Error fetching candidates:', error);
      } finally {
        setLoading(false);
      }
    };

    if (token) {
      fetchCandidates();
    }
  }, [token]);

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h1>Candidates</h1>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {candidates.map(candidate => (
            <tr key={candidate.candidateId}>
              <td>{candidate.candidateId}</td>
              <td>{`${candidate.firstName} ${candidate.lastName}`}</td>
              <td>{`${candidate.email}`}</td>
              <td>
                <Link to={`/admin/candidates/${candidate.candidateId}`} className="btn btn-primary">
                  View
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default CandidatesList;
