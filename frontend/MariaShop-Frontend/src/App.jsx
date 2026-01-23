import aveMaria from "./assets/monogram-NoBack.png";
import "./App.css";

function App() {
  return (
    <>
      <div>
        <a
          href="https://www.youtube.com/watch?v=i496eN1xhvo&list=PLxrDY5_Iho4CeOCoop0OnW7Q9M8ZYHKyJ&index=7"
          target="_blank"
        >
          <img src={aveMaria} className="logo" alt="Ave Maria" />
        </a>
      </div>
      <h1>MariaShop</h1>
      <p className="read-the-docs">Under development</p>
    </>
  );
}

export default App;
