import React, { useContext } from 'react';
import AuthContext from '../AuthContext';

const ExamPage = () => {
  return (
    <div id="exampage" className="container mt-5">
      <div className="row">
        {/* Left side with question list acting as a sidebar */}
        <div className="col-md-3">
          <div className="list-group" style={{overflowY : "scroll" , maxHeight : "80%" , padding : "1em" }}>
            <button type="button" className="list-group-item list-group-item-action active mb-1">
              Question 1
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            <button type="button" className="list-group-item list-group-item-action mb-1">
              Question 2
            </button>
            {/* Add more questions as needed */}
          </div>
        </div>
        {/* Right side with selected question and answers */}
        <div className="col-md-9">
          <div className="border p-3">
            <h4>MCQ Quiz</h4>
            <div className="d-flex flex-row align-items-center question-title">
              <h3 className="text-danger">1. Question 1</h3>
            </div>
            <div className="ans ml-2">
              {/* Option 1 */}
              <label className="options">Option A
                <input type="radio" name="radio" />
                <span className="checkmark"></span>
              </label>
              {/* Option 2 */}
              <label className="options">Option B
                <input type="radio" name="radio" />
                <span className="checkmark"></span>
              </label>
              {/* Option 3 */}
              <label className="options">Option C
                <input type="radio" name="radio" />
                <span className="checkmark"></span>
              </label>
              {/* Option 4 */}
              <label className="options">Option D
                <input type="radio" name="radio" />
                <span className="checkmark"></span>
              </label>
              {/* Add more options as needed */}
            </div>
            <div className="d-flex flex-row justify-content-between align-items-center p-3 bg-white">
              <button className="btn btn-primary d-flex align-items-center btn-danger" type="button">
                <i className="fa fa-angle-left mt-1 mr-1"></i>&nbsp;previous
              </button>
              <button className="btn btn-primary border-success align-items-center btn-success" type="button">
                Next<i className="fa fa-angle-right ml-2"></i>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ExamPage;
