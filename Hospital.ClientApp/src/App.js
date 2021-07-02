
import { makeStyles } from '@material-ui/core/styles';
import { AppBar, CssBaseline, Toolbar, Button } from '@material-ui/core';
import { AuthService } from "./service/auth";
import { ToastContainer } from 'react-toastify';
import { useUserContext } from "./context/UserContext";
import { useHistory, useLocation } from 'react-router-dom';
import Router from './Router';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  menu: {
    flexGrow: 1,
  },
  wrap: {
    marginTop: theme.spacing(12)
  }
}));

function App() {
  const classes = useStyles()

  const history = useHistory()
  const location = useLocation();

  const { user, setUser } = useUserContext()

  const authService = new AuthService();

  const logout = () => {
    authService.logout()
    setUser(null)
    history.push("/")
  }

  return (
    <div className={classes.wrap}>
      <CssBaseline />
      <AppBar position="fixed">
        <ToastContainer
          position="top-center"
          autoClose={2500}
          hideProgressBar={true}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable={false}
          pauseOnHover={false}
        />
        <Toolbar>
          <div className={classes.menu}>
            <Button data-cy="header-homepage" onClick={() => history.push('/')} color="inherit"  >
              Hospital
            </Button>
            {user && <Button data-cy="header-dashboard" onClick={() => history.push('/dashboard')} color="inherit"  >
              Dashboard
            </Button>}
          </div>
          {location.pathname !== "/login" && (user ?
            <Button data-cy="header-logout" onClick={logout} color="inherit">Logout</Button> :
            <Button onClick={() => history.push('/login')} color="inherit">Login</Button>)}
        </Toolbar>
      </AppBar>
      <Router />
    </div>
  );
}

export default App;