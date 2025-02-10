import { useState } from "react";

function AggregationTable() {

  // event handler
  return (
    <>
    <div className="header">
    <table >
    <tr>
    <th> 1</th>
    <th>2</th>  
    <th> 1</th>  
    <th> 1</th>  
    <th>1</th>  
  </tr>
  <tr>
    <th> <img className="tableIcon" src="../../imgs/very_happy.png" alt="" /></th>
    <th> <img className="tableIcon" src="../../imgs/happy.png" alt="" /></th>  
    <th> <img className="tableIcon" src="../../imgs/medium.png" alt="" /></th>  
    <th> <img className="tableIcon" src="../../imgs/sad.png" alt="" /></th>  
    <th> <img className="tableIcon" src="../../imgs/very_sad.png" alt="" /></th>  
  </tr>
  
</table>
      </div>
    </>
  );
}

export default AggregationTable;
