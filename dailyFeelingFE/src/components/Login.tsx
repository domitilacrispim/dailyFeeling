import { useState } from "react";
// items: []. heading
interface LoginProps {
  items: string[];
  heading: string;
}
function Login({ items, heading }: LoginProps) {
  const [selectedIndex, setSelectedIndex] = useState(-1);

  // event handler
  return (
    <>
    <div className="header">
          <img className="icon" src="../../imgs/flower.png" alt="" />
          <div className="text">Daily Feelings</div>
          <div className="underline"></div>
        </div>
      <div className="container">
        <div className="inputs">
        <p></p>
        <p></p>
        <p></p>
          <div className="input">
            <p>Email</p>
            <input className="inputField" type="email" placeholder="Email" />
          </div>
           <p></p>
          <div className="input">
            <p>Password</p>
            <input className="inputField" type="password" placeholder="Password"/>
          </div>

        </div>
        <p></p>
        <div className="forgot-password">
          Forgot password? <span className="clickable"> Click here</span>{" "}
        </div>
        <div className="submit-container">
        <p></p><p></p><p></p>
          <div className="submit">
          <button className="submit" name="button">Login</button> </div>
        </div>
      </div>
    </>
  );
}

export default Login;
