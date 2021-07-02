import axios from "axios"
import { FeedbackService } from "./feedback"
jest.mock("axios")

describe("FeedbackService", () => {
    const user = {
        id: 4,
        token: "adoiaufnonjsivfnzsmviksszm"
    }
    const headers = {
        Authorization: `Bearer ${user.token}`
    }

    it('should test constructor', () => {

        const create = jest.fn(() => null)
        axios.create = create

        const feedbackService = new FeedbackService(user);

        expect(feedbackService.user).toBe(user);
        expect(feedbackService.httpClient).toBe(null);
        expect(create).toHaveBeenCalled();
    })

    it('should test generateAuthHeaders func', () => {

        const feedbackService = new FeedbackService(user);

        expect(feedbackService.generateAuthHeaders()).toStrictEqual(headers);
    })

    it('should test getPublishedFeedback func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const feedbackService = new FeedbackService(user);
        feedbackService.getPublishedFeedback()

        expect(get).toHaveBeenCalled();
    })

    it('should test getAllFeedback func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const feedbackService = new FeedbackService(user);
        feedbackService.getAllFeedback()

        expect(get).toHaveBeenCalled();
    })

    it('should test postFeedback func', () => {

        const post = jest.fn()
        axios.create = jest.fn(() => ({ post }))

        const feedbackService = new FeedbackService(user);
        feedbackService.postFeedback()

        expect(post).toHaveBeenCalled();
    })

    it('should test updateFeedback func', () => {

        const get = jest.fn()
        axios.create = jest.fn(() => ({ get }))

        const feedbackService = new FeedbackService(user);
        feedbackService.updateFeedback()

        expect(get).toHaveBeenCalled();
    })

})