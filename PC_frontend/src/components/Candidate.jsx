import React, { useState, useEffect, useRef, useContext } from 'react';
import CandidateList from './CandidateList';
import AuthContext from '../AuthContext';

const CandidatesPage = () => {
  const { user, token } = useContext(AuthContext);

  const [candidates, setCandidates] = useState([
    {
      id: 1,
      Name: user?.name || '',
      MiddleName: user?.middleName || '',
      LastName: user?.lastName || '',
      Gender: user?.gender || '',
      NativeLanguage: user?.nativeLanguage || '',
      BirthDate: user?.birthDate || '',
      PhotoIDType: user?.photoIDType || '',
      PhotoIDNumber: user?.photoIDNumber || '',
      PhotoIDIssueDate: user?.photoIDIssueDate || '',
      Email: user?.email || '',
      Address: user?.address || '',
      Address2: user?.address2 || '',
      Country: user?.country || '',
      State: user?.state || '',
      Town: user?.town || '',
      PostalCode: user?.postalCode || '',
      LandlineNumber: user?.landlineNumber || '',
      MobileNumber: user?.mobileNumber || '',
      editing: false,
    },
  ]);

  const [candidateAdded, setCandidateAdded] = useState(false);
  const candidateRef = useRef(null);

  useEffect(() => {
    if (candidateRef.current && candidateAdded) {
      candidateRef.current.scrollIntoView({ behavior: 'smooth' });
      setCandidateAdded(false);
    }
  }, [candidateAdded]);

  const handleDelete = (id) => {
    setCandidates(candidates.filter(candidate => candidate.id !== id));
  };

  const handleAddCandidate = () => {
    setCandidates([...candidates, { ...defaultCandidate, editing: true }]);
    setCandidateAdded(true);
  };

  const handleEdit = (id, updatedCandidate) => {
    setCandidates(prevCandidates => {
      return prevCandidates.map(candidate => {
        if (candidate.id === id) {
          return {
            ...candidate,
            ...updatedCandidate,
            editing: false,
          };
        }
        return candidate;
      });
    });
  };

  const defaultCandidate = {
    id: candidates.length + 1,
    Name: user?.name || '',
    MiddleName: user?.middleName || '',
    LastName: user?.lastName || '',
    Gender: user?.gender || '',
    NativeLanguage: user?.nativeLanguage || '',
    BirthDate: user?.birthDate || '',
    PhotoIDType: user?.photoIDType || '',
    PhotoIDNumber: user?.photoIDNumber || '',
    PhotoIDIssueDate: user?.photoIDIssueDate || '',
    Email: user?.email || '',
    Address: user?.address || '',
    Address2: user?.address2 || '',
    Country: user?.country || '',
    State: user?.state || '',
    Town: user?.town || '',
    PostalCode: user?.postalCode || '',
    LandlineNumber: user?.landlineNumber || '',
    MobileNumber: user?.mobileNumber || '',
    editing: false,
  };

  return (
    <>
      <h1 className="text-center m-3 text-danger">User Control Panel</h1>
      {token && (
        <button className="btn btn-primary m-1" onClick={handleAddCandidate}>
          Update Information
        </button>
      )}
      <CandidateList candidates={candidates} onDelete={handleDelete} onEdit={handleEdit} />
      <div ref={candidateRef}></div>
    </>
  );
};

export default CandidatesPage;
