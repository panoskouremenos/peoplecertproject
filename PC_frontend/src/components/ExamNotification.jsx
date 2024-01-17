import React from 'react';

const ExamNotification = ({ time }) => {
    return(
        <div id="examNotification" >
            <h2 className="text-center">C# Fundamentals Exam Starting in : <b style={{ color : "darkred"}}>{time}</b></h2>
        </div>
    )
}

export default ExamNotification;