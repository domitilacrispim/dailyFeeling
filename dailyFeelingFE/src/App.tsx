import ListGroup from "./components/ListGroup";
import Login from "./components/Login";
import "./App.css";

function App() {
  let items = ["Rio", "Sao Paulo", "Tokyo", "Uberlandia"];
  return (
    <div>
      <Login items={items} heading="Cities" />
    </div>
  );
}

export default App;
