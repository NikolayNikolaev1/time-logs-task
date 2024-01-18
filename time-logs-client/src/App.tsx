import { ApplicationProvider } from "./context/ApplicationContext";
import Header from "./components/Header/Header";
import Content from "./components/Content";

const App = () => {
  return (
    <ApplicationProvider>
      <Header />
      <Content />
    </ApplicationProvider>
  );
};

export default App;
