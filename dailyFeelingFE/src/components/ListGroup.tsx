import { useState } from "react";
// items: []. heading
interface ListGroupProps {
  items: string[];
  heading: string;
}
function ListGroup({ items, heading }: ListGroupProps) {
  const [selectedIndex, setSelectedIndex] = useState(-1);

  // event handler
  return (
    <>
      <h1>{heading}</h1> {items.length === 0 && "Hello"}
      <ul className="list-group">
        {items.map((item, index) => (
          <li
            className={
              selectedIndex === index
                ? "list-group-item active"
                : "list-group-item"
            }
            key={item}
            onClick={() => {
              setSelectedIndex(index);
            }}
          >
            {item}
          </li>
        ))}
      </ul>
    </>
  );
}

export default ListGroup;
