import { ReactNode } from "react";
//@ts-ignore
import { Loading } from "react-loading-wrapper";
import "react-loading-wrapper/dist/index.css";
import useApplicationContext from "../../context/ApplicationContext";

const Loader = ({ children }: { children: ReactNode }) => {
  const { isLoading } = useApplicationContext();

  return (
    <Loading loading={isLoading} fullPage={true}>
      {children}
    </Loading>
  );
};

export default Loader;
