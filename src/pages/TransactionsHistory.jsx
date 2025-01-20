import { Box, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useRef, useState } from "react";
import axios from "axios";


const TransactionsHistory = (props) => {
    const firstLoad = useRef(true);
    const [transactionsHistoryList, setTransactionsHistoryList] = useState([
        {
            fullNameHeb: "",
            fullNameEng: "",
            birthDate: new Date(),
            userId: "",
            actionType: 0,
            amount: "1200",
            accountNumber: "",
        }
    ]);

    const [pageNumber, setPageNumber] = useState(1);
    const [totalTransactions, setTotalTransacitons] = useState(1);

    const getAllTransactions = async (url, payload) => {
        let res = await axios.post(url, payload);
        if (res.statusCode === 1) {
            setTotalTransacitons(res.total_Transactions);
            setTransactionsHistoryList(res.transactions_List);
        }
    };

    const handlePageChange = (e, page) => {
        setPageNumber(page);
        getAllTransactions("https://localhost:5001/Transaction/GetAllTransactions", {
            userId: props.userId,
            pageNumber: page,
            pageSize: 10
        });
    }

    useEffect(() => {

        if (firstLoad) {
            firstLoad.current = false;
            getAllTransactions("https://localhost:5001/Transaction/GetAllTransactions", {
                userId: props.userId,
                pageNumber: pageNumber,
                pageSize: 10
            });

        }

    }, []);

    return (
        <Box>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>תאריך</TableCell>
                            <TableCell align="right">סטטוס</TableCell>
                            <TableCell align="right">סכום</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {transactionsHistoryList.map((row, index) => (
                            <TableRow
                                key={index}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {`${row.birthDate}`}
                                </TableCell>
                                <TableCell align="right">{row.actionType === 0 ? "הפקדה" : "משיכה"}</TableCell>
                                <TableCell align="right">{row.amount}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Pagination sx={{ py: 5 }} count={totalTransactions} page={pageNumber} onChange={handlePageChange} />
        </Box>);
}

export default TransactionsHistory;