import React, { useState } from "react";
import { Form } from "react-bootstrap";
import html2pdf from 'html2pdf.js';

const Certificate = ({ certificate, onDelete, onEdit }) => {
  const [editing, setEditing] = useState(false);
  const [editedCertificate, setEditedCertificate] = useState({
    ...certificate,
  });

  const handleEditClick = () => {
    setEditing(true);
  };

  const handleSaveClick = () => {
    onEdit(certificate.id, editedCertificate);
    setEditing(false);
  };

  const handleCancelClick = () => {
    setEditing(false);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    const [fieldName, nestedField , index] = name.split('.');
    const updatedCertificate = {...editedCertificate};
    if (nestedField) {
        if (fieldName === 'topics') {
            if(editedCertificate[fieldName][nestedField][index] !== null){
            updatedCertificate[fieldName][nestedField][index] = value;
            setEditedCertificate(updatedCertificate)
            }
        }
    }else{
        updatedCertificate[fieldName] = value;
        setEditedCertificate(updatedCertificate)
    }
  };
  
  const handleAddTopic = () => {
    const updatedCertificate = {...editedCertificate};
    updatedCertificate.topics.push({
        description : '',
        awardedMarks : 0,
        possibleMarks : 50
    })
    setEditedCertificate(updatedCertificate);
  }

  const handleDeleteTopic = (index) => {
    setEditedCertificate((prevCertificate) => {
      const updatedTopics = prevCertificate.topics.filter((key, i) => i !== index);
      return {
        ...prevCertificate,
        topics: updatedTopics,
      };
    });
  };
  

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      handleSaveClick();
    }
  };

  const handleExportToPdf = (id) => {
    const element = document.getElementById(`candidateCert_${id}`);
    if (!element) {
      console.error('Element not found');
      return;
    }

    const options = {
      margin: [25, 70, 25, 70],
      filename: 'candidate_certification.pdf',
      image: { type: 'jpeg', quality: 0.98 },
      html2canvas: { scale: 3 },
      jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' },
    };

    html2pdf(element, options);
  };

  return (
    <div className="card m-2 text-center">
      <div className="card-body">
        {editing ? (
          <div className="d-flex flex-column gap-3">
            <Form.Group className="mb-3">
              <Form.Label>Certificate Title:</Form.Label>
              <Form.Control
                type="text"
                name="title"
                value={editedCertificate.title}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Candidate's First Name:</Form.Label>
              <Form.Control
                className="m-1"
                type="text"
                name="candidateNameFirst"
                placeholder="First Name"
                value={editedCertificate.candidateNameFirst}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Candidate's Middle Name:</Form.Label>
              <Form.Control
                className="m-1"
                type="text"
                name="candidateNameMiddle"
                placeholder="Middle Name"
                value={editedCertificate.candidateNameMiddle}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Candidate's Last Name:</Form.Label>
              <Form.Control
                className="m-1"
                type="text"
                name="candidateNameLast"
                placeholder="Last Name"
                value={editedCertificate.candidateNameLast}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Candidate Number:</Form.Label>
              <Form.Control
                type="text"
                name="candidateNumber"
                value={editedCertificate.candidateNumber}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Assessment Test Code:</Form.Label>
              <Form.Control
                type="text"
                name="assessmentTestCode"
                value={editedCertificate.assessmentTestCode}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Examination Date:</Form.Label>
              <Form.Control
                type="text"
                name="examinationDate"
                value={editedCertificate.examinationDate}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Score Report Date:</Form.Label>
              <Form.Control
                type="text"
                name="scoreReportDate"
                value={editedCertificate.scoreReportDate}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Candidate Score:</Form.Label>
              <Form.Control
                type="number"
                min="0"
                name="candidateScore"
                value={editedCertificate.candidateScore}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Maximum Score:</Form.Label>
              <Form.Control
                type="number"
                min="0"
                name="maximumScore"
                value={editedCertificate.maximumScore}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Percentage Score:</Form.Label>
              <Form.Control
                type="number"
                min="0"
                name="percentageScore"
                value={editedCertificate.percentageScore}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Assessment Result Label:</Form.Label>
              <Form.Control
                type="text"
                name="assessmentResultLabel"
                value={editedCertificate.assessmentResultLabel}
                onChange={(e) => handleInputChange(e)}
                onKeyDown={handleKeyPress}
              />
            </Form.Group>

            <Form.Group className="mb-3">
            <Form.Label>Topics:</Form.Label>
            <div>
            <button className="btn btn-primary m-1" onClick={handleAddTopic}>Add Topic</button>
            </div>
            {Object.keys(editedCertificate.topics).map((key , i) => (
                <div key={i}>
                    <h5 className="text-left">#{parseInt(i+1)}</h5>
                    <Form.Label>Topic Description:</Form.Label>
                <Form.Control
                    className="mb-1"
                    type="text"
                    title="Description"
                    placeholder="Topic Description"
                    name={`topics.${key}.description`}
                    value={editedCertificate.topics[key].description}
                    onChange={(e) => handleInputChange(e)}
                    onKeyDown={handleKeyPress}
                />
                <Form.Label>Awarded Marks:</Form.Label>
                <Form.Control
                    className="mb-1"
                    type="number"
                    min="0"
                    title="Awarded Marks"
                    name={`topics.${key}.awardedMarks`}
                    value={editedCertificate.topics[key].awardedMarks}
                    onChange={(e) => handleInputChange(e)}
                    onKeyDown={handleKeyPress}
                />
                <Form.Label>Possible Marks:</Form.Label>
                <Form.Control
                    className="mb-1"
                    type="number"
                    min="0"
                    title="Possible Marks"
                    name={`topics.${key}.possibleMarks`}
                    value={editedCertificate.topics[key].possibleMarks}
                    onChange={(e) => handleInputChange(e)}
                    onKeyDown={handleKeyPress}
                />
                <button className="btn btn-danger m-1" onClick={(e) => handleDeleteTopic(i)}>Delete</button>
                </div>
            ))}
            </Form.Group>
            <div>
              <button className="btn btn-success m-1" onClick={handleSaveClick}>
                Save
              </button>
              <button
                className="btn btn-secondary m-1"
                onClick={handleCancelClick}
              >
                Cancel
              </button>
            </div>
          </div>
        ) : (
          <>
          <div id={`candidateCert_${certificate.id}`}>
            <h2 className="mt-2 mb-4">{certificate.title}</h2>
            <p>
              Candidate Name: <b>{certificate.candidateNameFirst}{" "}
              {certificate.candidateNameMiddle}{" "}
              {certificate.candidateNameLast}</b>
            </p>
            <p>Candidate Number: <b>{certificate.candidateNumber}</b></p>
            <p>Assessment Test Code: <b>{certificate.assessmentTestCode}</b></p>
            <p>Examination Date: <b>{certificate.examinationDate}</b></p>
            <p>Score Report Date: <b>{certificate.scoreReportDate}</b></p>
            <p>Candidate Score: <b>{certificate.candidateScore}</b></p>
            <p>Maximum Score: <b>{certificate.maximumScore}</b></p>
            <p>Percentage Score: <b>{certificate.percentageScore}%</b></p>
            <p>Assessment Result Label: <b>{certificate.assessmentResultLabel}</b></p>
            <div>
              <h5>Topics:</h5>
              <ul className="list-unstyled">
                {certificate.topics.map((topic, index) => (
                  <li key={index}>
                    <strong>{topic.description}:</strong> {topic.awardedMarks}/
                    {topic.possibleMarks}
                  </li>
                ))}
              </ul>
            </div>
          </div>
            <div>
              <button className="btn btn-primary m-1" onClick={handleEditClick}>
                Edit
              </button>
              <button
                className="btn btn-danger m-1"
                onClick={() => onDelete(certificate.id)}
              >
                Delete
              </button>
              <button className="btn btn-success m-1" onClick={(e) => handleExportToPdf(certificate.id)}>Export PDF File</button>
            </div>
            </>
        )}
      </div>
    </div>
  );
};

export default Certificate;
