export const parseDate = (date: string) => {
  const currentDate = new Date(date);

  const year: number = currentDate.getFullYear();
  const month: string = ("0" + (currentDate.getMonth() + 1)).slice(-2);
  const day: string = ("0" + currentDate.getDate()).slice(-2);

  return day + "." + month + "." + year;
};
