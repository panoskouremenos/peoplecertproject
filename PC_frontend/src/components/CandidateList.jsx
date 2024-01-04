import React from 'react';
import Candidate from './Candidate';

const CandidateList = ({ candidates, onDelete, onEdit }) => {
  return (
    <div >
      {candidates.map(candidate => (
        <Candidate key={candidate.id} candidate={candidate} onDelete={onDelete} onEdit={onEdit} />
      ))}
    </div>
  );
};

export default CandidateList;