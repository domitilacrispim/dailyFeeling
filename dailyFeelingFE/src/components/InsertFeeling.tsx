import { useState } from "react";

function InsertFeeling() {


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
    <th> <img className="tableIcon" src="../../imgs/very_happy.png" alt="" onClick={() =>InsertFeelingOnDb(1)}/></th>
    <th> <img className="tableIcon" src="../../imgs/happy.png" alt="" onClick={() =>InsertFeelingOnDb(2)}/></th>  
    <th> <img className="tableIcon" src="../../imgs/medium.png" alt="" onClick={() =>InsertFeelingOnDb(3)}/></th>  
    <th> <img className="tableIcon" src="../../imgs/sad.png" alt="" onClick={() =>InsertFeelingOnDb(4)}/></th>  
    <th> <img className="tableIcon" src="../../imgs/very_sad.png" alt="" onClick={() =>InsertFeelingOnDb(5)}/></th>  
  </tr>
  
</table>
      </div>
    </>
  );
}
function InsertFeelingOnDb(feelingId: number) {
  console.log(feelingId)
  fetch('http://localhost:5005/api/Feelings/CreateFeeling', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Authorization': 'Bearer ' + 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJsb2JsQGpmZHNqaW9mc2RqLmNvbSIsInVzZXJuYW1lIjoiYmxvYmx2IiwidXNlcklkIjoiMiIsIm5iZiI6MTczOTczMDU0OSwiZXhwIjoxNzM5NzM0MTQ5LCJpYXQiOjE3Mzk3MzA1NDl9.MLCukLph1Aiqi_IZ0Gjug38KLK3Y98eJqGfJMUfHQYU'
    },
    body: JSON.stringify({
      firstParam: '2',
    })
  })
  .then(response => response.json())  // Converte a resposta para JSON
  .then(data => {
    console.log('Resposta da requisição:', data);  // Aqui você tem acesso à resposta
    // Aqui você pode atualizar o estado ou fazer o que for necessário com os dados
  })
  .catch(error => {
    console.error('Erro na requisição:', error);
  });
}


export default InsertFeeling;
