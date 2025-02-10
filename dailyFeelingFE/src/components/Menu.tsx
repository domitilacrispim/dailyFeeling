import { useState } from "react";

function Menu() {
  const [selectedIndex, setSelectedIndex] = useState(-1);

  // event handler
  return (
    <>
    <div className="header">
    <table className="tableMenu">
    <tr>
    <th> <img className="menuIcon" src="../../imgs/people.png" alt="" /></th>
    <th> <img className="menuIcon" src="../../imgs/home.png" alt="" /></th>
  </tr>
</table>
      </div>
    </>
  );
}

export default Menu;
