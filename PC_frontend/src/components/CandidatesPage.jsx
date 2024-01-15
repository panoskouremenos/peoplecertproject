// import React, { useState , useEffect, useRef } from 'react';
// import CandidateList from './CandidateList';

// const CandidatesPage = () => {
//   const [candidates, setCandidates] = useState([
//     {
//         id: 1,
//         Name: 'John',
//         MiddleName: 'M.',
//         LastName: 'Doe',
//         Gender: 'Male',
//         NativeLanguage: 'English',
//         BirthDate: '1990-05-15',
//         PhotoIDType: 'National Card',
//         PhotoIDNumber: '123456789',
//         PhotoIDIssueDate: '2020-01-01',
//         Email: 'john.doe@example.com',
//         Address: '123 Main St',
//         Address2: 'Apt 4B',
//         Country: 'USA',
//         State: 'California',
//         Town: 'Los Angeles',
//         PostalCode: '90001',
//         LandlineNumber: '555-1234',
//         MobileNumber: '555-5678',
//       },
//       {
//         id: 2,
//         Name: 'Alice',
//         MiddleName: 'A.',
//         LastName: 'Smith',
//         Gender: 'Female',
//         NativeLanguage: 'Spanish',
//         BirthDate: '1988-08-22',
//         PhotoIDType: 'Passport',
//         PhotoIDNumber: '987654321',
//         PhotoIDIssueDate: '2019-02-15',
//         Email: 'alice.smith@example.com',
//         Address: '456 Oak St',
//         Address2: 'Suite 102',
//         Country: 'Canada',
//         State: 'Ontario',
//         Town: 'Toronto',
//         PostalCode: 'M5V 1A1',
//         LandlineNumber: '416-555-7890',
//         MobileNumber: '416-555-1234',
//       },
//       {
//         id: 3,
//         Name: 'David',
//         MiddleName: 'D.',
//         LastName: 'Johnson',
//         Gender: 'Male',
//         NativeLanguage: 'French',
//         BirthDate: '1985-12-10',
//         PhotoIDType: 'National Card',
//         PhotoIDNumber: '555555555',
//         PhotoIDIssueDate: '2018-07-05',
//         Email: 'david.johnson@example.com',
//         Address: '789 Pine St',
//         Address2: 'Apt 7C',
//         Country: 'France',
//         State: 'Île-de-France',
//         Town: 'Paris',
//         PostalCode: '75001',
//         LandlineNumber: '+33 1 23 45 67 89',
//         MobileNumber: '+33 6 12 34 56 78',
//       }
//   ]);

//   const [candidateAdded, setCandidateAdded] = useState(false);
//   const candidateRef = useRef(null);

//   useEffect(() => {
//     if (candidateRef.current && candidateAdded) {
//       candidateRef.current.scrollIntoView({ behavior: "smooth" });
//       setCandidateAdded(false);
//     }
//   }, [candidateAdded]);

//   const handleDelete = (id) => {
//     setCandidates(candidates.filter(candidate => candidate.id !== id));
//   };

//   const handleAddCandidate = () => {
//     setCandidates([...candidates, {
//       id: candidates.length + 1,
//       Name: '',
//       MiddleName: '',
//       LastName: '',
//       Gender: 'Male',
//       NativeLanguage: '',
//       BirthDate: '',
//       PhotoIDType: '',
//       PhotoIDNumber: '',
//       PhotoIDIssueDate: '',
//       Email: '',
//       Address: '',
//       Address2: '',
//       Country: '',
//       State: '',
//       Town: '',
//       PostalCode: '',
//       LandlineNumber: '',
//       MobileNumber: '',
//     }]);
//     setCandidateAdded(true);
//   };

//   const handleEdit = (id, updatedCandidate) => {
//     setCandidates(prevCandidates => {
//       return prevCandidates.map(candidate => {
//         if (candidate.id === id) {
//           return {
//             ...candidate,
//             ...updatedCandidate,
//           };
//         }
//         return candidate;
//       });
//     });
//   };

//   return (
//     <>
//       <h1 className="text-center m-3 text-danger">Candidates</h1>
//       <button className="btn btn-primary m-1" onClick={handleAddCandidate}>Add Candidate</button>
//       <CandidateList candidates={candidates} onDelete={handleDelete} onEdit={handleEdit} />
//       <div ref={candidateRef}></div>
//     </>
//   );
// };

// export default CandidatesPage;