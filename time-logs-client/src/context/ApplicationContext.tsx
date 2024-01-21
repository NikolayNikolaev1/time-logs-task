import { ReactNode, createContext, useContext, useState } from "react";

interface DateRange {
  startDate: string;
  endDate: string;
}

interface ChartBarProps {
  label: string;
  value: number;
}

interface ApplicationContextProps {
  dateRangeFilter: DateRange | null;
  handleDateRangeChange(newDateRange: DateRange | null): void;
  comparedChartBar: ChartBarProps | null;
  handleComparedChartBarChange(newChartBar: ChartBarProps | null): void;
  isLoading: boolean;
  startLoading(): void;
}

const ApplicationContext = createContext({} as ApplicationContextProps);

const useApplicationContext = () => useContext(ApplicationContext);

export const ApplicationProvider = ({ children }: { children: ReactNode }) => {
  const [dateRangeFilter, setDateRangeFilter] = useState<DateRange | null>(
    null,
  );
  const [comparedChartBar, setComparedChartBar] =
    useState<ChartBarProps | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const handleDateRangeChange = (newDateRange: DateRange | null) => {
    setDateRangeFilter(newDateRange);
  };

  const handleComparedChartBarChange = (newChartBar: ChartBarProps | null) => {
    setComparedChartBar(newChartBar);
  };

  const startLoading = () => setIsLoading(true);

  return (
    <ApplicationContext.Provider
      value={{
        dateRangeFilter,
        handleDateRangeChange,
        comparedChartBar,
        handleComparedChartBarChange,
        isLoading,
        startLoading,
      }}
    >
      {children}
    </ApplicationContext.Provider>
  );
};

export default useApplicationContext;
