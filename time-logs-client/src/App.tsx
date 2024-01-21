import { ApplicationProvider } from "./context/ApplicationContext";
import Header from "./components/Header/Header";
import Content from "./components/Content";
import Loader from "./components/Loader";

const App = () => {
  return (
    <ApplicationProvider>
      <Loader>
        <Header />
        <Content />
      </Loader>
    </ApplicationProvider>
  );
};

export default App;
