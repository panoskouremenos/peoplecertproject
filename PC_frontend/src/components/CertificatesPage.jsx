import React, { useState , useEffect, useRef } from 'react';
import CertificateList from './CertificateList';

const CertificatesPage = () => {
  const [certificates, setCertificates] = useState([
    {
        id: 1,
        title: 'Java Certification',
        candidateNameFirst: 'John',
        candidateNameMiddle: 'M.',
        candidateNameLast: 'Doe',
        candidateNumber: '123456',
        assessmentTestCode: 'JAVA123',
        examinationDate: '2022-05-10',
        scoreReportDate: '2022-05-15',
        candidateScore: 90,
        maximumScore: 100,
        percentageScore: 90,
        assessmentResultLabel: 'Pass',
        topics: [
          {
            description: 'Java Basics',
            awardedMarks: 45,
            possibleMarks: 50,
          },
        ],
      },
      {
        id: 2,
        title: 'React Certification',
        candidateNameFirst: 'Alice',
        candidateNameMiddle: 'A.',
        candidateNameLast: 'Smith',
        candidateNumber: '654321',
        assessmentTestCode: 'REACT987',
        examinationDate: '2022-06-15',
        scoreReportDate: '2022-06-20',
        candidateScore: 95,
        maximumScore: 100,
        percentageScore: 95,
        assessmentResultLabel: 'Pass',
        topics: [
          {
            description: 'React Components',
            awardedMarks: 50,
            possibleMarks: 50,
          },
        ],
      },
      {
        id: 3,
        title: 'JavaScript Certification',
        candidateNameFirst: 'David',
        candidateNameMiddle: 'D.',
        candidateNameLast: 'Johnson',
        candidateNumber: '789012',
        assessmentTestCode: 'JS789',
        examinationDate: '2022-07-20',
        scoreReportDate: '2022-07-25',
        candidateScore: 92,
        maximumScore: 100,
        percentageScore: 92,
        assessmentResultLabel: 'Pass',
        topics: [
          {
            description: 'JavaScript Fundamentals',
            awardedMarks: 46,
            possibleMarks: 50,
          },
          {
            description: 'C# Advanced',
            awardedMarks : 35,
            possibleMarks : 55,
          }
        ],
      }
  ]);


  const [certificateAdded, setCertificateAdded] = useState(false);
  const certificateRef = useRef(null);

  useEffect(() => {
    if (certificateRef.current && certificateAdded) {
      certificateRef.current.scrollIntoView({ behavior: "smooth" });
      setCertificateAdded(false);
    }
  }, [certificateAdded]);
  

  const handleDelete = (id) => {
    setCertificates(certificates.filter(certificate => certificate.id !== id));
  };
  const handleAddCertificate = () => {
    setCertificates([...certificates, {
      id: certificates.length + 1,
      title: 'Test',
      candidateNameFirst: '',
      candidateNameMiddle: '',
      candidateNameLast: '',
      candidateNumber: '',
      assessmentTestCode: '',
      examinationDate: '',
      scoreReportDate: '',
      candidateScore: 0,
      maximumScore: 100,
      percentageScore: 0,
      assessmentResultLabel: '',
      topics: [],
    }]);
    setCertificateAdded(true);
  };
  
  const handleEdit = (id, updatedCertificate) => {
    setCertificates(prevCertificates => {
      return prevCertificates.map(certificate => {
        if (certificate.id === id) {
          return {
            ...certificate,
            ...updatedCertificate,
          };
        }
        return certificate;
      });
    });
  };

  return (
    <>
    <h1 className="text-center m-3 text-danger">Certificates</h1>
      <button className="btn btn-primary m-1" onClick={handleAddCertificate}>Add Certificate</button>
      <CertificateList certificates={certificates} onDelete={handleDelete} onEdit={handleEdit} />
      <div ref={certificateRef}></div>
    </>
  );
};

export default CertificatesPage;