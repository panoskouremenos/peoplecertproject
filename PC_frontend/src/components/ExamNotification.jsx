import React from 'react';

const ExamNotification = ({ data }) => {
    return(
        <div id="examNotification" >        

            <h2 className="text-center">{data.certificateTitle} Exam Starting in : <b style={{ color : "darkred"}}>{data.timeRemaining}</b> minutes.
            <a href={`/exam/${data.examID}`}>GO!</a>
            </h2>
        </div>
    )
}

export default ExamNotification;