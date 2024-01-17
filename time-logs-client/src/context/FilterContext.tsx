import { ReactNode, createContext, useContext, useState } from "react";

interface DateRange {
  startDate: string;
  endDate: string;
}

interface FilterContextProps {
  dateRange: DateRange | null;
  handleDateRangeChange(newDateRange: DateRange | null): void;
}

const FilterContext = createContext({} as FilterContextProps);

const useFilterContext = () => useContext(FilterContext);

export const FilterProvider = ({ children }: { children: ReactNode }) => {
  const [dateRange, setDateRange] = useState<DateRange | null>(null);

  const handleDateRangeChange = (newDateRange: DateRange | null) => {
    setDateRange(newDateRange);
  };

  return (
    <FilterContext.Provider value={{ dateRange, handleDateRangeChange }}>
      {children}
    </FilterContext.Provider>
  );
};

export default useFilterContext;
