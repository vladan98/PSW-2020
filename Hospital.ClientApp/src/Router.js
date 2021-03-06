import React from 'react'
import Home from "./pages/home"; import Login from "./pages/login"
import PatientDashboard from "./pages/dashboard/patient"
import AdminDashboard from "./pages/dashboard/admin"
import DoctorDashboard from "./pages/dashboard/doctor"
import AddFeedback from "./pages/patient/feedback/add"
import { Switch, Route, Redirect } from "react-router-dom"
import { useUserContext } from './context/UserContext';
import Register from './pages/register';
import AppointmentsList from './pages/patient/appointments';
import ScheduleAppointment from './pages/patient/appointments/add';
import Recipes from './pages/doctor/recipes';
import Referrals from './pages/doctor/referral/add';
import PatientsList from './pages/admin/patients';
import FeedbackList from './pages/admin/feedback';

const NotFound = () => <div>404 Not found</div>

const PatientRoutes = () => (
    <>
        <Route exact path="/patient/dashboard" component={PatientDashboard} />
        <Route exact path="/patient/appointments" component={AppointmentsList} />
        <Route exact path="/patient/appointments/add" component={ScheduleAppointment} />
        <Route exact path="/patient/feedback/add" component={AddFeedback} />
    </>
)

const DoctorRoutes = () => (
    <>
        <Route exact path="/doctor/dashboard" component={DoctorDashboard} />
        <Route exact path="/doctor/recipes" component={Recipes} />
        <Route exact path="/doctor/referral/add" component={Referrals} />
    </>
)

const AdminRoutes = () => (
    <>
        <Route exact path="/admin/dashboard" component={AdminDashboard} />
        <Route exact path="/admin/patients" component={PatientsList} />
        <Route exact path="/admin/feedback" component={FeedbackList} />
    </>
)

const Router = () => {
    const { user } = useUserContext()

    const getUserDashboard = () => {
        if (user?.role === "ADMIN") return <Redirect to="/admin/dashboard" />
        else if (user?.role === "PATIENT") return <Redirect to="/patient/dashboard" />
        else if (user?.role === "DOCTOR") return <Redirect to="/doctor/dashboard" />
        else return <NotFound />
    }

    return (
        <Switch>
            <Route exact path="/" component={Home} />
            <Route exact path="/login" component={Login} />
            <Route exact path="/register" component={Register} />

            {(!user || user.id === -1) && <Route path="*" >
                <Redirect to="/login" />
            </Route>}

            <Route exact path="/dashboard"  >
                {getUserDashboard()}
            </Route>

            {user && user.role === "PATIENT" && <PatientRoutes />}
            {user && user.role === "ADMIN" && <AdminRoutes />}
            {user && user.role === "DOCTOR" && <DoctorRoutes />}

            <Route path="*" component={NotFound} />
        </Switch>
    )
}

export default Router
