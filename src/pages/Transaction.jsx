import { Typography, Box, TextField, Select, MenuItem, Button } from "@mui/material";
import Grid from '@mui/material/Grid2';
import { useState } from "react";
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { he } from 'date-fns/locale';
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider/LocalizationProvider";
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import usePostRequest from "../hooks/usePostRequest";



const Transaction = (props) => {

    const [transactionDetails, setTransactionDetails] = useState({
        fullNameHeb: "",
        fullNameEng: "",
        birthDate: new Date(),
        userId: "",
        actionType: 0,
        amount: "",
        accountNumber: "",
    });

    const { accountNumber, actionType, amount, fullNameEng, fullNameHeb, userId } = transactionDetails;

    const DepositURL = "https://localhost:5001/Transaction/Deposit";
    const WithdrawalURL = "https://localhost:5001/Transaction/Withdrawal";

    const { data, loading, error, postRequest } = usePostRequest({});

    const handleChangeTransactionDetails = (e) => {
        setTransactionDetails({ ...transactionDetails, [e.target.name]: e.target.value });
    }

    const handleChangeBirthDate = (TValue, TError, value, context) => {
        console.log(TValue, TError, value, context);
        setTransactionDetails({ ...transactionDetails, birthDate: TValue });

    }

    const handleSendReq = () => {
        switch (actionType) {
            case 0:
                // show message of no action choose.!
                return;
            case 1:
                postRequest(DepositURL, { transactionData: { ...transactionDetails } });
                break;
            case 2:
                postRequest(WithdrawalURL, { transactionData: { ...transactionDetails } });
                break;
            default:
                break;
        }
        
    }


    return <Box>
        <Grid container direction="column" justifyContent="center" alignItems="center" columnSpacing={2} rowSpacing={4}>
            <Grid>
                <Typography sx={{ fontSize: "20px" }}>העברת כספים מול ספק חיצוני</Typography>
            </Grid>
            <Grid container direction="row-reverse" justifyContent="flex-start" alignItems="flex-start">
                <Grid container direction="column" rowSpacing={2}>
                    <Grid><TextField variant="outlined" size="small" label="שם מלא בעברית" name="fullNameHeb" onChange={handleChangeTransactionDetails} value={fullNameHeb} /></Grid>
                    <Grid><TextField variant="outlined" size="small" label="שם מלא באנגלית" name="fullNameEng" onChange={handleChangeTransactionDetails} value={fullNameEng} /></Grid>
                    <Grid>
                        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={he}>
                            <DatePicker name="birthDate" onChange={handleChangeBirthDate} disableFuture label="תאריך לידה" format="DD/MM/YYYY"
                                slotProps={{
                                    textField: {
                                        sx: {
                                            '& .MuiInputBase-root': {
                                                height: '40px',
                                            },
                                        },
                                    },
                                }}

                                renderInput={(params) => <TextField {...params} />}
                            />
                        </LocalizationProvider>
                    </Grid>
                </Grid>
                <Grid container direction="column" rowSpacing={2}>
                    <Grid>
                        <TextField variant="outlined" size="small" label="ת.ז" name="userId" onChange={handleChangeTransactionDetails} value={userId} /></Grid>
                    <Grid>
                        <Select size="small" label="סוג פעולה" name="actionType" value={actionType} onChange={handleChangeTransactionDetails}>
                            <MenuItem value={0}>בחר פעולה</MenuItem>
                            <MenuItem value={1}>הפקדה</MenuItem>
                            <MenuItem value={2}>משיכה</MenuItem>
                        </Select>
                    </Grid>
                    <Grid><TextField variant="outlined" size="small" label="סכום" name="amount" onChange={handleChangeTransactionDetails} value={amount} /></Grid>
                    <Grid><TextField variant="outlined" size="small" label="מספר חשבון" name="accountNumber" onChange={handleChangeTransactionDetails} value={accountNumber} /></Grid>
                </Grid>
            </Grid>
            <Grid>
                <Button variant="contained" size="large" onClick={handleSendReq}>שלח</Button>
            </Grid>
        </Grid>
    </Box>;
}

export default Transaction;