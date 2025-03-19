import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Cookies from "js-cookie";
import { Button } from "@mui/material";

import {
	CookiesConstants,
	HeaderPageConstants,
	LoginPageConstants,
} from "@helpers/Constants";
import { GetUserAsync, UserDataFailure } from "@store/Users/Actions";
import Spinner from "@components/Common/Spinner";
import Toaster from "@components/Common/Toaster";
import FooterComponent from "@components/Common/Footer";
import UserLoginDtoModel from "@models/UserLoginDto";

/**
 * @component
 * LoginComponent handles the user login functionality.
 *
 * @param {Object} props - The properties passed to the component.
 *
 * @returns {JSX.Element} The rendered component.
 */
function LoginComponent(props) {
	const dispatch = useDispatch();

	const UserStoreData = useSelector((state) => state.UsersReducer.userData);
	const UserErrorStoreData = useSelector(
		(state) => state.UsersReducer.userDataError
	);
	const IsDataLoading = useSelector(
		(state) => state.UsersReducer.isUserDataLoading
	);

	const [data, setData] = useState({
		UserEmail: "",
		UserPassword: "",
	});
	const [errors, setErrors] = useState({
		UserEmail: "",
		UserPassword: "",
	});
	const [errorState, setErrorState] = useState("");

	useEffect(() => {
		if (
			UserStoreData !== null &&
			UserStoreData !== undefined &&
			Object.keys(UserStoreData).length > 0
		) {
			props.history.push(HeaderPageConstants.Headings.Home.Link);
			Cookies.set(
				CookiesConstants.LoggedInUser.Name,
				JSON.stringify(UserStoreData),
				{ expires: CookiesConstants.LoggedInUser.Timeout }
			);
		}
	}, [UserStoreData]);

	useEffect(() => {
		if (errorState !== UserErrorStoreData) {
			setErrorState(UserErrorStoreData);
		}
	}, [UserErrorStoreData]);

	/**
	 * Handles the form change event.
	 * @param {Event} event The on change event.
	 */
	const handleFormChange = (event) => {
		event.persist();
		const target = event.target;
		setData({
			...data,
			[target.name]: target.value,
		});
	};

	/**
	 * Handles the form submit event.
	 * @param {Event} event The submit event.
	 */
	const handleSubmit = (event) => {
		event.preventDefault();

		const validations = LoginPageConstants.validations;
		errors.UserEmail =
			data.UserEmail === "" ? validations.UserEmailRequired : "";
		errors.UserPassword =
			data.UserPassword === "" ? validations.UserPasswordRequired : "";
		setErrors({ ...errors });

		if (errors.UserEmail === "" && errors.UserPassword === "") {
			const loginData = new UserLoginDtoModel(
				data.UserEmail,
				data.UserPassword
			);

			dispatch(GetUserAsync(loginData));
		}
	};

	/**
	 * Clears the error message.
	 */
	const clearErrorMessage = () => {
		dispatch(UserDataFailure(""));
	};

	return (
		<div className="container">
			<Spinner isLoading={IsDataLoading} />
			<Toaster
				errorMessage={errorState}
				clearErrorMessage={clearErrorMessage}
			/>
			<div className="row">
				<div className="col-sm-12 mt-5">
					<h1 className="architectDaughterfont text-center">
						{LoginPageConstants.Headings.LoginUser}
					</h1>
				</div>

				<form className="loginuser">
					<div className="form-group row">
						<div className="col-sm-6 mb-3 mb-sm-0">
							<div className="row"></div>
							<div className="row p-2">
								<label
									htmlFor="UserEmail"
									className="form-label"
								>
									Email <span className="red">*</span>
								</label>
								<input
									type="email"
									name="UserEmail"
									onChange={handleFormChange}
									value={data.UserEmail}
									className="form-control mt-0 ml-10"
									id="UserEmail"
									placeholder="Email"
								/>
								{errors.UserEmail && (
									<span className="alert alert-danger ml-10 mt-2">
										{errors.UserEmail}
									</span>
								)}
							</div>
						</div>

						<div className="col-sm-6 mb-3 mb-sm-0">
							<div className="row p-2 ">
								<label
									htmlFor="UserPassword"
									className="form-label"
								>
									Password <span className="red">*</span>
								</label>
								<input
									type="password"
									name="UserPassword"
									onChange={handleFormChange}
									value={data.UserPassword}
									className="form-control mt-0 ml-10"
									id="UserPassword"
									placeholder="Password"
								/>
								{errors.UserPassword && (
									<span className="alert alert-danger ml-10 mt-2">
										{errors.UserPassword}
									</span>
								)}
							</div>
						</div>
					</div>

					<div className="text-center">
						<Button
							variant="contained"
							className="mt-3"
							onClick={handleSubmit}
						>
							{LoginPageConstants.Headings.LoginButton}
						</Button>
					</div>
				</form>
			</div>

			<FooterComponent />
		</div>
	);
}
function Login() {
	return <></>;
}
export default LoginComponent;
